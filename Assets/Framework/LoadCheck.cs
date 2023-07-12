
namespace VampireSLike
{
    public class LoadCheck
    {
        private bool _isReady=false;

        public LoadCheck()
        {
        }
        public LoadCheck(string name)
        {
            this.Name = name;
        }
        public void SetReady()
        {
            _isReady = true;
        }
        public string Name { get; set; }
        public bool State { get => _isReady; }
    }
}
