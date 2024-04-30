namespace Todo.application.Helpers
{
    public static class Transform<T> where T : class
    {
        public static T Copy(T src, T des)
        {
            var properties = typeof(T).GetProperties().Where(x => x.GetValue(src) != null);

            foreach (var property in properties)
            {
                var srcValue = property.GetValue(src);
                var desValue = property.GetValue(des);

                if (srcValue != desValue)
                    property.SetValue(des, srcValue);
            }
            var ok = des;
            return des;
        }
    }
}
