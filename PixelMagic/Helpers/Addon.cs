namespace PixelMagic.Helpers
{
    public static class Addon
    {
        public static string LuaContents = @"
local size = 5;	-- this is the size of the ""pixels"" at the top of the screen that will show stuff, currently 5x5 because its easier to see and debug with

-- Actual Addon Code below
local f = CreateFrame(""frame"")
f:SetSize(5 * size, size);  -- Width, Height
f:SetPoint(""TOPLEFT"", 0, 0)
f:RegisterEvent(""ADDON_LOADED"")

local hpframes = {}
local cooldownframes = {}
local spellInRangeFrames = {}
local healthFrames = {}
local targetHealthFrames = {}
local isTargetFriendlyFrame = nil
local hasTargetFrame = nil
local powerFrames = {}
local playerIsCastingFrame = nil
local targetIsCastingFrame = nil

local hpPrev = 0
local lastCooldownState = {}

local function updateHP()
	local power = UnitPower(""player"", 9)
	
	if power ~= hpPrev then	
		print(""Holy Power: "" .. power)   
	   
		local i = 1

		while i <= power do
			hpframes[i].t:SetTexture(255, 0, 0, 1)
			hpframes[i].t:SetAllPoints(false)
			i = 1 + i
		end
		
		while i <= 5 do
			hpframes[i].t:SetTexture(0, 255, 255, 1)
			hpframes[i].t:SetAllPoints(false)
			i = 1 + i
		end
		
		hpPrev = power
	 end
 end

local function updateCD() 
	for _, spellId in pairs(cooldowns) do
		-- start is the value of GetTime() at the point the spell began cooling down
		-- duration is the total duration of the cooldown, NOT the remaining
		local start, duration, enable = GetSpellCooldown(spellId)
		if start and duration then -- the spell is on cooldown
			local getTime = GetTime()

			-- start + duration gives us the value of GetTime() at the point when the cd will end
			-- the time when the cd ends (a time in the future) minus the current time gives us the remaining duration on the cooldown
			local remainingCD = start + duration - getTime

			if (remainingCD > 0) then
																-- if (spellId == 642) Divine Shield is on CD
																-- BUG: when trigger global CD on ANY spell this increases to 1 second
																-- dont have a workaround for now
				if (lastCooldownState[spellId] ~= ""onCD"") then										 
					--print(""Spell with Id = "" .. spellId .. "" is on CD: "" .. remainingCD)
					
					cooldownframes[spellId].t:SetTexture(255, 0, 0, 1)
					cooldownframes[spellId].t:SetAllPoints(false)
					
					lastCooldownState[spellId] = ""onCD""
				end				
			else
				if (lastCooldownState[spellId] ~= ""offCD"") then
					--print(""Spell with Id = "" .. spellId .. "" is off CD and can be cast"")
					
					cooldownframes[spellId].t:SetTexture(255, 255, 255, 1)
					cooldownframes[spellId].t:SetAllPoints(false)
					
					lastCooldownState[spellId] = ""offCD""
				end
			end				
		end
	end
end

local lastSpellInRange = {}

local function updateSpellInRangeFrames() 
	for _, spellId in pairs(cooldowns) do
		
		local name, rank, icon, castTime, minRange, maxRange = GetSpellInfo(spellId)
		
		-- http://wowwiki.wikia.com/wiki/API_IsSpellInRange	
		local inRange = IsSpellInRange(name, ""target"")  -- '0' if out of range, '1' if in range, or 'nil' if the unit is invalid. 
								
		if lastSpellInRange[spellId] ~= inRange then
			if (inRange == 1) then
				spellInRangeFrames[spellId].t:SetTexture(255, 0, 0, 1)
			else
				spellInRangeFrames[spellId].t:SetTexture(255, 255, 255, 1)
			end 
			spellInRangeFrames[spellId].t:SetAllPoints(false)
			
			--print(""Spell: "" .. name .. "" InRange = "" .. inRange)
			
			lastSpellInRange[spellId] = inRange	
		end				
	end
end

function healthToBinary(num)
	local bits = 7

    -- returns a table of bits
    local t={} -- will contain the bits
    for b=bits,1,-1 do
        rest=math.fmod(num,2)
        t[b]=rest
        num=(num-rest)/2
    end
	
	return table.concat(t)
end

local lastHealth = 0

local function updateHealth()
	local health = UnitHealth(""player"");		
	local maxHealth = UnitHealthMax(""player"");
	local percHealth = ceil((health / maxHealth) * 100)
	
	if (percHealth ~= lastHealth) then		
		local binaryHealth = healthToBinary(percHealth)
		print (""Health = "" .. percHealth .. "" binary = "".. binaryHealth)
		
		for i = 1, string.len(binaryHealth) do
			local currentBit = string.sub(binaryHealth, i, i)
			
			if (currentBit == ""1"") then
				healthFrames[i].t:SetTexture(255, 0, 0, 1)
			else
				healthFrames[i].t:SetTexture(255, 255, 255, 1)
			end
			healthFrames[i].t:SetAllPoints(false)
		end
		
		lastHealth = percHealth
	end
end

local lastTargetHealth = 0

local function updateTargetHealth()
	local guid = UnitGUID(""target"")
	local health = UnitHealth(""target"");		
	local maxHealth = UnitHealthMax(""target"");
	local percHealth = ceil((health / maxHealth) * 100)
	
	if (guid == nil) then
		percHealth = 0
	end
	
	if (percHealth ~= lastTargetHealth) then		
		local binaryHealth = healthToBinary(percHealth)
		--print (""Target Health = "" .. percHealth .. "" binary = "".. binaryHealth)
		
		for i = 1, string.len(binaryHealth) do
			local currentBit = string.sub(binaryHealth, i, i)
			
			if (currentBit == ""1"") then
				targetHealthFrames[i].t:SetTexture(255, 0, 0, 1)
			else
				targetHealthFrames[i].t:SetTexture(255, 255, 255, 1)
			end
			targetHealthFrames[i].t:SetAllPoints(false)
		end
		
		lastTargetHealth = percHealth
	end
end

local lastPower = 0
local playerClass, englishClass, classIndex = UnitClass(""player"");
local currentSpec = GetSpecialization()
local currentSpecId = currentSpec and select(1, GetSpecializationInfo(currentSpec)) or 0

local function updatePower()
	local power = UnitPower(""player"");		
	local maxPower = UnitPowerMax(""player"");
	
	-- For testing 120 we will hardcode to add 20 to current power since we testing on a hunter
	-- power = power + 20
	
	if (power ~= lastPower) then
		lastPower = power
			
		-- If the class uses mana, then we need to calculate percent mana to show its power
		-- http://wowwiki.wikia.com/wiki/API_UnitClass
		-- http://wowwiki.wikia.com/wiki/SpecializationID
		if (
			(classIndex == 7)  -- Shaman   
		 or (classIndex == 2)  -- Paladin
		 or (classIndex == 5)  -- Priest 
		 or (classIndex == 8)  -- Mage
		 or (classIndex == 9)  -- Lock
		 or ((classIndex == 11) and (currentSpecId == 102)) -- Druid Balance
		 or ((classIndex == 11) and (currentSpecId == 105)) -- Druid Resto 
		   ) 
		then 
			power = ceil((power / maxPower) * 100)
		end
		
		local binaryPower = healthToBinary(power)			
		--print (""Power = "" .. power .. "" binary = "".. binaryPower)	
		--print (""Current Spec = "" .. currentSpecId)
		
		for i = 1, string.len(binaryPower) do
			local currentBit = string.sub(binaryPower, i, i)
			
			if (currentBit == ""1"") then
				powerFrames[i].t:SetTexture(255, 0, 0, 1)
			else
				powerFrames[i].t:SetTexture(255, 255, 255, 1)
			end
			powerFrames[i].t:SetAllPoints(false)
		end	
	end
end
 
local lastIsFriend = true 
 
local function updateIsFriendly()
	isFriend = UnitIsFriend(""player"",""target"");
	
	if (isFriend ~= lastIsFriend) then
	
		if (isFriend == true) then
			print (""Unit is friendly: True"")
			
			isTargetFriendlyFrame.t:SetTexture(0, 255, 0, 1)
		else
			print (""Unit is friendly: False"")
			
			isTargetFriendlyFrame.t:SetTexture(0, 0, 255, 1)
		end
	
		lastIsFriend = isFriend
	end
end

local lastTargetGUID = """"

local function hasTarget()
	guid = UnitGUID(""target"")
		
	if (guid ~= lastTargetGUID) then
		if (guid == nil) then
			--print (""Target GUID: None"" )	
			
			hasTargetFrame.t:SetTexture(0, 0, 0, 1)
		else			
			--print (""Target GUID: "" .. guid )	
			
			hasTargetFrame.t:SetTexture(255, 0, 0, 1)
		end
			
		lastTargetGUID = guid		
	end
end

local lastCastID = 0

local function updatePlayerIsCasting()
	spell, rank, displayName, icon, startTime, endTime, isTradeSkill, castID, interrupt = UnitCastingInfo(""player"")
	
	if castID ~= nil then	
		if castID ~= lastCastID then
			--print(""Casting spell: "" .. spell)
		
			playerIsCastingFrame.t:SetTexture(255, 0, 0, 1)
		
			lastCastID = castID		
		end
	else
		if castID ~= lastCastID then
			--print(""Not casting"")
			
			playerIsCastingFrame.t:SetTexture(255, 255, 255, 1)
			
			lastCastID = castID		
		end	
	end		
end

local lastTargetCastID = 0

local function updateTargetIsCasting()	
	spell, rank, displayName, icon, startTime, endTime, isTradeSkill, castID, interrupt = UnitCastingInfo(""target"")
		
	if castID ~= nil then	
		if castID ~= lastTargetCastID then
			print(""Casting spell: "" .. spell)
		
			targetIsCastingFrame.t:SetTexture(255, 0, 0, 1)
		
			lastTargetCastID = castID		
		end
	else
		if castID ~= lastTargetCastID then
			print(""Not casting"")
			
			targetIsCastingFrame.t:SetTexture(255, 255, 255, 1)
			
			lastTargetCastID = castID		
		end	
	end
end
 
local function initFrames()
	print (""Initialising Holy Power Frames"")
	for i = 1, 5 do
		hpframes[i] = CreateFrame(""frame"");
		hpframes[i]:SetSize(size, size)
		hpframes[i]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)        
		hpframes[i].t = hpframes[i]:CreateTexture()        
		hpframes[i].t:SetTexture(0, 255, 255, 1)
		hpframes[i].t:SetAllPoints(hpframes[i])
		hpframes[i]:Show()
		
		hpframes[i]:SetScript(""OnUpdate"", updateHP)
	end
	
	print (""Initialising Cooldown Frames"")
	local i = 5
	for _, spellId in pairs(cooldowns) do	
		cooldownframes[spellId] = CreateFrame(""frame"")
		cooldownframes[spellId]:SetSize(size, size)
		cooldownframes[spellId]:SetPoint(""TOPLEFT"", i * size, 0)        
		cooldownframes[spellId].t = cooldownframes[spellId]:CreateTexture()        
		cooldownframes[spellId].t:SetTexture(255, 255, 255, 1)
		cooldownframes[spellId].t:SetAllPoints(cooldownframes[spellId])
		cooldownframes[spellId]:Show()
		               
		cooldownframes[spellId]:SetScript(""OnUpdate"", updateCD)
		i = i + 1
	end
	
	print (""Initialising Spell In Range Frames"")
	local i = 0
	for _, spellId in pairs(cooldowns) do	
		spellInRangeFrames[spellId] = CreateFrame(""frame"")
		spellInRangeFrames[spellId]:SetSize(size, size)
		spellInRangeFrames[spellId]:SetPoint(""TOPLEFT"", i * size, -size * 5)        
		spellInRangeFrames[spellId].t = spellInRangeFrames[spellId]:CreateTexture()        
		spellInRangeFrames[spellId].t:SetTexture(255, 255, 255, 1)
		spellInRangeFrames[spellId].t:SetAllPoints(spellInRangeFrames[spellId])
		spellInRangeFrames[spellId]:Show()
		               
		spellInRangeFrames[spellId]:SetScript(""OnUpdate"", updateSpellInRangeFrames)
		i = i + 1
	end
	
	print (""Initialising Health Frames"")
	for i = 1, 7 do
		healthFrames[i] = CreateFrame(""frame"")
		healthFrames[i]:SetSize(size, size)
		healthFrames[i]:SetPoint(""TOPLEFT"", (i - 1) * size, -size)        
		healthFrames[i].t = healthFrames[i]:CreateTexture()        
		healthFrames[i].t:SetTexture(255, 255, 255, 1)
		healthFrames[i].t:SetAllPoints(healthFrames[i])
		healthFrames[i]:Show()		
		
		healthFrames[i]:SetScript(""OnUpdate"", updateHealth)
	end
	
	print (""Initialising Target Health Frames"")
	for i = 1, 7 do
		targetHealthFrames[i] = CreateFrame(""frame"")
		targetHealthFrames[i]:SetSize(size, size)
		targetHealthFrames[i]:SetPoint(""TOPLEFT"", (i - 1) * size, -size * 4)        
		targetHealthFrames[i].t = targetHealthFrames[i]:CreateTexture()        
		targetHealthFrames[i].t:SetTexture(255, 255, 255, 1)
		targetHealthFrames[i].t:SetAllPoints(targetHealthFrames[i])
		targetHealthFrames[i]:Show()		
		
		targetHealthFrames[i]:SetScript(""OnUpdate"", updateTargetHealth)
	end
	
	-- Power can go above 100, it can be 120 maximum to my knowledge
	print (""Initialising Power Frames (Rage, Energy, etc...)"")  
	for i = 1, 7 do
		powerFrames[i] = CreateFrame(""frame"")
		powerFrames[i]:SetSize(size, size)
		powerFrames[i]:SetPoint(""TOPLEFT"", (i - 1) * size, -size * 3)        
		powerFrames[i].t = powerFrames[i]:CreateTexture()        
		powerFrames[i].t:SetTexture(255, 255, 255, 1)
		powerFrames[i].t:SetAllPoints(powerFrames[i])
		powerFrames[i]:Show()		
		
		powerFrames[i]:SetScript(""OnUpdate"", updatePower)
	end
	
	print (""Initialising IsTargetFriendly Frame"")
	isTargetFriendlyFrame = CreateFrame(""frame"");
	isTargetFriendlyFrame:SetSize(size, size);
	isTargetFriendlyFrame:SetPoint(""TOPLEFT"", 0, -(size * 2))    
	isTargetFriendlyFrame.t = isTargetFriendlyFrame:CreateTexture()        
	isTargetFriendlyFrame.t:SetTexture(0, 255, 0, 1)
	isTargetFriendlyFrame.t:SetAllPoints(isTargetFriendlyFrame)
	isTargetFriendlyFrame:Show()		
		
	isTargetFriendlyFrame:SetScript(""OnUpdate"", updateIsFriendly)
	
	print (""Initialising HasTarget Frame"")
	hasTargetFrame = CreateFrame(""frame"");
	hasTargetFrame:SetSize(size, size);
	hasTargetFrame:SetPoint(""TOPLEFT"", size, -(size * 2))    
	hasTargetFrame.t = hasTargetFrame:CreateTexture()        
	hasTargetFrame.t:SetTexture(0, 255, 0, 1)
	hasTargetFrame.t:SetAllPoints(hasTargetFrame)
	hasTargetFrame:Show()		
		
	hasTargetFrame:SetScript(""OnUpdate"", hasTarget)
	
	print (""Initialising PlayerIsCasting Frame"")
	playerIsCastingFrame = CreateFrame(""frame"");
	playerIsCastingFrame:SetSize(size, size);
	playerIsCastingFrame:SetPoint(""TOPLEFT"", size * 2, -(size * 2))    
	playerIsCastingFrame.t = playerIsCastingFrame:CreateTexture()        
	playerIsCastingFrame.t:SetTexture(255, 255, 255, 1)
	playerIsCastingFrame.t:SetAllPoints(playerIsCastingFrame)
	playerIsCastingFrame:Show()		
		
	playerIsCastingFrame:SetScript(""OnUpdate"", updatePlayerIsCasting)
	
	print (""Initialising TargetIsCasting Frame"")
	targetIsCastingFrame = CreateFrame(""frame"");
	targetIsCastingFrame:SetSize(size, size);
	targetIsCastingFrame:SetPoint(""TOPLEFT"", size * 3, -(size * 2))    
	targetIsCastingFrame.t = targetIsCastingFrame:CreateTexture()        
	targetIsCastingFrame.t:SetTexture(255, 255, 255, 1)
	targetIsCastingFrame.t:SetAllPoints(targetIsCastingFrame)
	targetIsCastingFrame:Show()		
		
	targetIsCastingFrame:SetScript(""OnUpdate"", updateTargetIsCasting)
	
	print (""Initialization Complete"")
end

local function eventHandler(self, event, ...)
	local arg1 = ...
	if event == ""ADDON_LOADED"" then
		if (arg1 == ""DoIt"") then
			print(""Addon Loaded... DoIt"")
			print(""Tracking "" .. table.getn(cooldowns) .. "" cooldowns"")
			initFrames()
		end
	end
end	

f:SetScript(""OnEvent"", eventHandler)";
    }
}
