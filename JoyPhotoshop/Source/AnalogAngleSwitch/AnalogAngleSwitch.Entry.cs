namespace JoyPhotoshop
{
    partial class AnalogAngleSwitch
    {
        class Entry
        {
            public readonly string identifier;
            public readonly Angle angle;

            public bool prevOn;
            public bool on;

            public Entry(string identifier, Angle angle)
            {
                this.identifier = identifier;
                this.angle = angle;
            }

            public override bool Equals(object obj)
            {
                var entry = obj as Entry;
                return entry != null && identifier == entry.identifier;
            }

            public override int GetHashCode() => identifier.GetHashCode();

            public static bool operator ==(Entry a, Entry b) => a.angle == b.angle;
            public static bool operator !=(Entry a, Entry b) => a.angle != b.angle;
        }
    }
}
