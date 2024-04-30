using HashidsNet;
using Todo.application.Abstractions;

namespace Todo.application.Helpers
{
    public class Hid : IHid
    {
        private readonly IHashids _hashids;

        public Hid(IHashids hashids)
        {
            _hashids = hashids;
        }

        public int Decode(string id)
        {
            var rawId = _hashids.Decode(id);

            if (rawId.Length == 0)
                return -1;

            return rawId[0];
        }

        public string Encode(int id)
        {
            return _hashids.Encode(id);
        }
    }
}
