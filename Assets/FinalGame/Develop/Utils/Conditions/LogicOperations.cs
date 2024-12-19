using UnityEditorInternal;

namespace FinalGame.Develop.Utils.Conditions
{
    public static class LogicOperations
    {
        public static bool AndOperation(bool previous, bool current) => previous && current;
        
        public static bool OrOperation(bool previous, bool current) => previous || current;
    }
}