using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace JoyPhotoshop
{
    partial class AnalogAngleSwitch
    {
        public Angle CurrentAngle { get; private set; }

        readonly Dictionary<string, Entry> entryDict = new Dictionary<string, Entry>();

        public void Add(string identity, Angle angle)
        {
            entryDict.Add(identity, new Entry(identity, angle));
        }

        public bool IsPress(string identifier) => entryDict[identifier].on;
        public bool IsDown(string identifier) => !entryDict[identifier].prevOn && entryDict[identifier].on;
        public bool IsUp(string identifier) => entryDict[identifier].prevOn && !entryDict[identifier].on;

        public void Update(Vector2 input)
        {
            CurrentAngle = Angle.FromVector(input);

            if (entryDict.Count == 0) return;

            var sqrLength = input.LengthSquared();
            var insideAreaToOn = sqrLength > 0.7 * 0.7;
            var insideAreaToOff = sqrLength < 0.5 * 0.5;

            var nearestEntry = entryDict.OrderBy(kvp =>
            {
                var normalizedInput = Vector2.Normalize(input);
                var normalizedAngle = Vector2.Normalize(kvp.Value.angle.ToVector());
                if (normalizedInput == Vector2.Zero || normalizedAngle == Vector2.Zero) return -2;
                return Vector2.Dot(normalizedInput, normalizedAngle);
            }).First().Value;

            foreach (var entry in entryDict.Values)
            {
                entry.prevOn = entry.on;

                if (entry != nearestEntry)
                {
                    entry.on = false;
                    continue;
                }

                if (entry.prevOn)
                {
                    if (insideAreaToOff)
                    {
                        entry.on = false;
                    }
                    else
                    {
                        entry.on = true;
                    }
                }
                else
                {
                    if (insideAreaToOn)
                    {
                        entry.on = true;
                    }
                    else
                    {
                        entry.on = false;
                    }
                }
            }
        }
    }
}
