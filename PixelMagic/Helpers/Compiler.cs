using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PixelMagic.Helpers
{
    public static class Compiler
    {
        public static T Compile<T>(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string code = sr.ReadToEnd();

                if (code.Trim() == "")
                {
                    throw new Exception("Please select a non blank file");
                }

                if (fileName.EndsWith(".enc"))
                {
                    Log.Write("Decrypting profile...", Color.Black);

                    try
                    {
                        code = Encryption.Decrypt(code);

                        Log.Write("Profile has been decrypted successfully", Color.Green);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex.Message, Color.Red);
                    }
                }

                Log.Write($"Compiling profile [{fileName}]...", Color.Black);

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();

                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");    
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");            
                parameters.ReferencedAssemblies.Add("System.Data.dll");
                parameters.ReferencedAssemblies.Add("System.Xml.dll");
                parameters.ReferencedAssemblies.Add("System.Linq.dll");
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Threading.dll");
                parameters.ReferencedAssemblies.Add(Application.ExecutablePath);

                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = false;

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (CompilerError error in results.Errors)
                    {
                        Log.Write($"Error ({error.ErrorNumber}): {error.ErrorText}", Color.Red);
                    }

                    throw new Exception("See error log");
                }

                Assembly assembly = results.CompiledAssembly;

                foreach (Type t in assembly.GetTypes())
                {
                    if (t.IsClass)
                    {
                        object obj = Activator.CreateInstance(t);

                        var abstractClass = (T)obj;

                        return abstractClass;
                    }
                }

                throw new Exception("See error log");
            }
        }
    }
}
