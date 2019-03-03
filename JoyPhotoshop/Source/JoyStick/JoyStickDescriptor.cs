using System;

namespace JoyPhotoshop
{
    class JoyStickDescriptor
    {
        public readonly Guid guid;
        public readonly string name;

        public JoyStickDescriptor(Guid guid, string name)
        {
            this.guid = guid;
            this.name = name;
        }

        public override string ToString() => $"{name} [{guid}]";
    }
}
