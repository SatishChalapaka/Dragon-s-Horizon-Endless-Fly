namespace MalbersAnimations
{
    public interface ILocalVars
    {


        public int GetInt(string name);
        public float GetFloat(string name);
        public bool GetBool(string name);
        public string GetString(string name);

        public void SetInt(string name, int value);
        public void SetFloat(string name, float value);
        public void SetBool(string name, bool value);

        public void SetString(string name, string value);

        public void SetVar(ILocalVar var);

        public bool SetVar<T>(string name, T value);
        public T GetVar<T>(string name);
    }

    public interface ILocalVar
    {
        public string Name { get; }

        public object GetValueRaw();
    }
}