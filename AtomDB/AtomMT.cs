using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomDB
{
    /// <summary>
    /// MultiThreaded Atom
    /// </summary>
    public class AtomMT
    {
        Dictionary<byte[], byte[]> database = new();

        public static AtomMT Create()
        {
            if (!AtomManager.Initialized) throw new("Atom not initialized, please call AtomManager.Initialize() before using any Atom functions!");
            return new()
            {
                database = new()
            };
        }

        public static AtomMT Load(string path)
        {
            if (!AtomManager.Initialized) throw new("Atom not initialized, please call AtomManager.Initialize() before using any Atom functions!");
            MemoryStream stream = new MemoryStream(File.ReadAllBytes(path));
            BinaryReader br = new BinaryReader(stream);
            ulong count = br.ReadUInt64();
            Dictionary<byte[], byte[]> db = new();
            for (ulong i = 0; i < count; i++)
            {
                var keyLength = br.ReadInt32();
                byte[] keyBytes = br.ReadBytes(keyLength);

                var valueLength = br.ReadInt32();
                byte[] valueBytes = br.ReadBytes(valueLength);

                db.Add(keyBytes, valueBytes);
            }
            br.Dispose();
            stream.Dispose();
            return new()
            {
                database = db
            };
        }

        public void Save(string path)
        {
            MemoryStream stream = new MemoryStream();
            var bw = new BinaryWriter(stream);
            bw.Write(database.LongCount());
            for (long i = 0; i < database.LongCount(); i++)
            {
                var key = database.ToArray()[i].Key;
                var value = database.ToArray()[i].Value;

                bw.Write(key.Length);
                bw.Write(key);

                bw.Write(value.Length);
                bw.Write(value);
            }
            File.WriteAllBytes(path, stream.ToArray());
            bw.Dispose();
            stream.Dispose();
        }

        public byte[] GetBytes(byte[] key)
        {
            if (!database.ContainsKey(key)) throw new("Key was not found.");
            return database[key];
        }

        public byte[] GetBytes(string key)
        {
            return GetBytes(Encoding.UTF8.GetBytes(key));
        }

        public void SetBytes(string key, byte[] value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                if (database.ContainsKey(Encoding.UTF8.GetBytes(key)))
                    database[Encoding.UTF8.GetBytes((string)key)] = value;
                else
                    database.Add(Encoding.UTF8.GetBytes((string)key), value);
            });
        }

        public void SetBytes(byte[] key, byte[] value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                if (database.ContainsKey(key))
                    database[key] = value;
                else
                    database.Add(key, value);
            });
        }

        public void SetInt(string key, int value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetInt(byte[] key, int value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetUInt(string key, uint value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetUInt(byte[] key, uint value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetULong(string key, ulong value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetULong(byte[] key, ulong value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetLong(string key, long value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetLong(byte[] key, long value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetShort(string key, short value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetShort(byte[] key, short value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetUShort(string key, ushort value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetUShort(byte[] key, ushort value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                SetBytes(key, BitConverter.GetBytes(value));
            });
        }

        public void SetString(string key, string value)
        {
            SetString(Encoding.UTF8.GetBytes(key), value);
        }

        public void SetString(byte[] key, string value)
        {
            ThreadPool.QueueUserWorkItem((object e) =>
            {
                MemoryStream ms = new();
                BinaryWriter bw = new(ms);
                bw.Write(value);
                var bytes = ms.ToArray();
                ms.Dispose();
                bw.Dispose();
                SetBytes(key, bytes);
            });
        }

        public int GetInt(string key)
        {
            return BitConverter.ToInt32(GetBytes(key));
        }

        public int GetInt(byte[] key)
        {
            return BitConverter.ToInt32(GetBytes(key));
        }

        public int GetShort(string key)
        {
            return BitConverter.ToInt16(GetBytes(key));
        }

        public int GetShort(byte[] key)
        {
            return BitConverter.ToInt16(GetBytes(key));
        }

        public int GetUShort(string key)
        {
            return BitConverter.ToUInt16(GetBytes(key));
        }

        public int GetUShort(byte[] key)
        {
            return BitConverter.ToUInt16(GetBytes(key));
        }

        public uint GetUInt(string key)
        {
            return BitConverter.ToUInt32(GetBytes(key));
        }

        public uint GetUInt(byte[] key)
        {
            return BitConverter.ToUInt32(GetBytes(key));
        }

        public long GetLong(string key)
        {
            return BitConverter.ToInt64(GetBytes(key));
        }

        public long GetLong(byte[] key)
        {
            return BitConverter.ToInt64(GetBytes(key));
        }

        public ulong GetULong(string key)
        {
            return BitConverter.ToUInt64(GetBytes(key));
        }

        public ulong GetULong(byte[] key)
        {
            return BitConverter.ToUInt64(GetBytes(key));
        }

        public string GetString(string key)
        {
            var bytes = GetBytes(key);
            MemoryStream ms = new(bytes);
            BinaryReader br = new(ms);
            var str = br.ReadString();
            ms.Dispose();
            br.Dispose();
            return str;
        }

        public string GetString(byte[] key)
        {
            var bytes = GetBytes(key);
            MemoryStream ms = new(bytes);
            BinaryReader br = new(ms);
            var str = br.ReadString();
            ms.Dispose();
            br.Dispose();
            return str;
        }
    }
}
