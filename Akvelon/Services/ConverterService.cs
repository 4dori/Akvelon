namespace Akvelon.Services
{
    public class ConverterService
    {        
        public static string ListToString(List<string> tasks)
        {
            string joinedTasks = tasks.Aggregate((a, b) => a + ", " + b);
            return joinedTasks;
        }

        public static List<string> StringToList(string joinedNames)
        {
            List<string> list = joinedNames.Split(", ").ToList();
            return list;
        }
        public static string ListIdToString(List<Guid> taskIds)
        {
            List<string> list = new();
            foreach (Guid i in taskIds)
            {
                string guid = i.ToString();
                list.Add(guid);

            }

            string joinedTasks = list.Aggregate((a, b) => a + ", " + b);
            return joinedTasks;
        }
        public static List<Guid> StringToListId(string joinedIds)
        {
            List<string> listIds = joinedIds.Split(", ").ToList();
            List<Guid> listGuids = new();
            foreach (string i in listIds)
            {
                Guid guid = Guid.Parse(i);
                listGuids.Add(guid);
            }

            return listGuids;
        }
    }
}
