using System.Reflection;
using System.Text;

namespace Lesson9;
class Program
{
    public static void Main()
    {
        Type type = typeof(TestClass);
        var s3 = Activator.CreateInstance(type, 7, new[] { 'c', 's', 's' }, "Hello", 10.1m);
        Console.WriteLine(ObjectToString(s3));
        var obj = StringToObject(ObjectToString(s3));
        Console.WriteLine(obj);
        Console.WriteLine(ObjectToString(obj));


        
    }

    public static void Task()
    {
        Type type = typeof(TestClass);
        var s1 = Activator.CreateInstance(type);
        var s2 = Activator.CreateInstance(type,  10);
        
    }
    public static object StringToObject(string objectInString)
    {
        string[] arrayInfo = objectInString.Split("\n");

        var obj = Activator.CreateInstance(null, arrayInfo[1])?.Unwrap();
        if (obj != null && arrayInfo.Length > 2)
        {
            Type type = obj.GetType();

            for (int i = 2; i < arrayInfo.Length; i++)
            {
                string[] parametrInfo = arrayInfo[i].Split("=");
                var prop = type.GetProperty(parametrInfo[0]);
                foreach (FieldInfo fieldinfo in obj.GetType().GetFields())
                {


                    CustomName? atribute = fieldinfo.GetCustomAttribute<CustomName>();
                    if(atribute?.Name == parametrInfo[0])
                    {
                        Type filedType = fieldinfo.FieldType;
                        object parseValue = Convert.ChangeType(parametrInfo[1], filedType);
                        fieldinfo.SetValue(obj, parseValue);
                    }
                }
                if (prop == null)
                {
                    continue;
                }
                else if (prop.PropertyType == typeof(int))
                {
                    prop.SetValue(obj, Int32.Parse(parametrInfo[1]));
                }
                else if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(obj, parametrInfo[1]);
                }
                else if (prop.PropertyType == typeof(char[]))
                {
                    prop.SetValue(obj, parametrInfo[1].ToCharArray());
                }
                else if (prop.PropertyType == typeof(decimal))
                {
                    prop.SetValue(obj, decimal.Parse(parametrInfo[1]));
                }
                else if (prop == null)
                {
                    
                    Type filedTipe = prop.GetType();
                    CustomName? atribute = type.GetCustomAttribute<CustomName>();
                   
                    prop.SetValue(obj, atribute.Name);
                }



            }

            
        }
        return obj;
    }

    public static string ObjectToString(object o)
    {
        StringBuilder objectToString = new StringBuilder();
        Type type = o.GetType();
        objectToString.Append(type.AssemblyQualifiedName + "\n");
        objectToString.Append(type.FullName + "\n");
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            CustomName? atribute = field.GetCustomAttribute<CustomName>();
            if (atribute != null)
            {
                string filedName = atribute.Name;
                objectToString.Append(atribute.Name + "=");
            }
            var val = field.GetValue(o);
            objectToString.Append(val + "\n");
        }
        var properties = type.GetProperties();
        foreach(var prop in properties)
        {
            CustomName? atribute = prop.GetCustomAttribute<CustomName>();
            if (atribute != null)
            {
                string filedName = atribute.Name;
                objectToString.Append(atribute.Name + "=");
            }
            else
            {

                objectToString.Append(prop.Name + "=");
            }
            var val = prop.GetValue(o);
            if(prop.PropertyType == typeof(char[])){
                objectToString.Append(new string(val as char[]) + "\n");
            }
            else
            {
                objectToString.Append(val + "\n");
            }
           
        }
        return objectToString.ToString();



    }
}

