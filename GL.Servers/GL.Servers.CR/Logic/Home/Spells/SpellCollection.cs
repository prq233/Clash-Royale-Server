﻿namespace GL.Servers.CR.Logic.Spells
{
    using System.Collections.Generic;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;

    using Newtonsoft.Json.Linq;

    internal class SpellCollection
    {
        private List<Spell> Spells;
        private int NextSerial;

        internal Spell this[int Index]
        {
            get
            {
                if (this.Spells.Count > Index)
                {
                    return this.Spells[Index];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the number of spells in collection.
        /// </summary>
        internal int Count
        {
            get
            {
                return this.Spells.Count;
            }
        }

        /// <summary>
        /// Gets the latest create time.
        /// </summary>
        internal int LatestCreateTime
        {
            get
            {
                int Max = 0;

                this.Spells.ForEach(Spell =>
                {
                    Max = Math.Max(Spell.CreateTime, Max);
                });

                return Max;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellCollection"/> class.
        /// </summary>
        internal SpellCollection()
        {
            this.Spells = new List<Spell>(40);
        }

        /// <summary>
        /// Adds the specified spell in collection.
        /// </summary>
        internal void AddSpell(Spell Spell)
        {
            if (!this.CanAddSpell(Spell))
            {
                Logging.Error(this.GetType(), "AddSpell() - Trying to add spell that already exists in collection, data:" + Spell.Data + ".");
            }

            this.Spells.Add(Spell);
        }

        /// <summary>
        /// Returns if can add the specified spell in collection.
        /// </summary>
        internal bool CanAddSpell(Spell Spell)
        {
            return !this.Spells.Contains(Spell);
        }

        /// <summary>
        /// Gets a spell by data.
        /// </summary>
        internal Spell GetSpellByData(SpellData Data)
        {
            return this.Spells.Find(Spell => Spell.Data == Data);
        }

        /// <summary>
        /// Gets the spell index in the collection by data.
        /// </summary>
        internal int GetSpellIdxByData(SpellData Data)
        {
            return this.Spells.FindIndex(Spell => Spell.Data == Data);
        }

        /// <summary>
        /// Removes the specified spell.
        /// </summary>
        internal void RemoveSpell(Spell Spell)
        {
            this.Spells.Remove(Spell);
        }

        /// <summary>
        /// Removes the element located at specified index..
        /// </summary>
        internal void RemoveSpell(int Idx)
        {
            this.Spells.RemoveAt(Idx);
        }

        /// <summary>
        /// Sets the spell at specified index.
        /// </summary>
        internal void SetSpell(int Index, Spell Spell)
        {
            this.Spells[Index] = Spell;
        }

        /// <summary>
        /// Swap two spells in collection.
        /// </summary>
        internal Spell SwapSpell(Spell Spell, int Index)
        {
            Spell Swap = this.Spells[Index];
            this.Spells[Index] = Spell;
            return Swap;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Reader)
        {
            for (int i = Reader.ReadVInt(); i > 0; i--)
            {
                Spell Spell = new Spell(null);
                Spell.Decode(Reader);
                this.Spells.Add(Spell);
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.AddVInt(this.Spells.Count);

            this.Spells.ForEach(Spell =>
            {
                Spell.Encode(Packet);
            });
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            if (JsonHelper.GetJsonArray(Json, "spells", out JArray Spells))
            {
                foreach (JToken Token in Spells)
                {
                    Spell Spell = new Spell(null);
                    Spell.Load(Token);
                    this.Spells.Add(Spell);
                }
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal void Save(JObject Json)
        {
            JArray Spells = new JArray();

            this.Spells.ForEach(Spell =>
            {
                Spells.Add(Spell.Save());
            });

            Json.Add("spells", Spells);
        }
    }
}