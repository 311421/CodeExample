using System.Collections.Generic;
using Isu.Objects;

namespace IsuExtra.Entities
{
    public class Faculty
    {
        public Faculty(string name)
        {
            Name = name;
        }

        public List<Stream> Streams { get; } = new List<Stream>();
        public string Name { get; }
        public List<Group> Groups { get; } = new List<Group>();

        public Stream CreateStream(string name)
        {
            Streams.Add(new Stream(name, this));
            return Streams[^1];
        }
    }
}