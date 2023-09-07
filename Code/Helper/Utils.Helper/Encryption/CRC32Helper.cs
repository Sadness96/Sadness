using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.Encryption
{
    /// <summary>
    /// 循环沉余效验帮助类
    /// 创建日期:2017年06月16日
    /// </summary>
    public class CRC32Helper
    {
        /// <summary>
        /// CRC32 加密(8位小写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>CRC32密文(8位小写)</returns>
        public static string CRC32Encrypt_8Lower(string strPlaintext)
        {
            try
            {
                Crc32 crc32Crypto = new Crc32();
                byte[] bytes_crc32_in = UTF8Encoding.Default.GetBytes(strPlaintext);
                byte[] bytes_crc32_out = crc32Crypto.ComputeHash(bytes_crc32_in);
                string str_crc32_out = BitConverter.ToString(bytes_crc32_out);
                str_crc32_out = str_crc32_out.Replace("-", "");
                str_crc32_out = str_crc32_out.ToLower();
                return str_crc32_out;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// CRC32 加密(8位大写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>CRC32 密文(8位大写)</returns>
        public static string CRC32Encrypt_8Upper(string strPlaintext)
        {
            try
            {
                Crc32 crc32Crypto = new Crc32();
                byte[] bytes_crc32_in = UTF8Encoding.Default.GetBytes(strPlaintext);
                byte[] bytes_crc32_out = crc32Crypto.ComputeHash(bytes_crc32_in);
                string str_crc32_out = BitConverter.ToString(bytes_crc32_out);
                str_crc32_out = str_crc32_out.Replace("-", "");
                str_crc32_out = str_crc32_out.ToUpper();
                return str_crc32_out;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文件 CRC32 值(8位小写)
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>文件 CRC32 值(8位小写)</returns>
        public static string FileCRC32Encrypt_8Lower(string strFilePath)
        {
            try
            {
                String hashCRC32 = String.Empty;
                // 检查文件是否存在,如果文件存在则进行计算,否则返回空值
                if (File.Exists(strFilePath))
                {
                    using (FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // 计算文件的 CSC32 值
                        Crc32 calculator = new Crc32();
                        Byte[] buffer = calculator.ComputeHash(fileStream);
                        calculator.Clear();
                        // 将字节数组转换成十六进制的字符串形式
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            stringBuilder.Append(buffer[i].ToString("x2"));
                        }
                        hashCRC32 = stringBuilder.ToString();
                    }
                }
                return hashCRC32;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文件 CRC32 值(8位大写)
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>文件 CRC32 值(8位大写)</returns>
        public static string FileCRC32Encrypt_8Upper(string strFilePath)
        {
            try
            {
                String hashCRC32 = String.Empty;
                // 检查文件是否存在,如果文件存在则进行计算,否则返回空值
                if (File.Exists(strFilePath))
                {
                    using (FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // 计算文件的 CSC32 值
                        Crc32 calculator = new Crc32();
                        Byte[] buffer = calculator.ComputeHash(fileStream);
                        calculator.Clear();
                        // 将字节数组转换成十六进制的字符串形式
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            stringBuilder.Append(buffer[i].ToString("X2"));
                        }
                        hashCRC32 = stringBuilder.ToString();
                    }
                }
                return hashCRC32;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 提供 CRC32 算法的实现
    /// </summary>
    public class Crc32 : System.Security.Cryptography.HashAlgorithm
    {
        /// <summary>
        /// Default Polynomial
        /// </summary>
        public const UInt32 DefaultPolynomial = 0xedb88320;
        /// <summary>
        /// Default Seed
        /// </summary>
        public const UInt32 DefaultSeed = 0xffffffff;
        private UInt32 hash;
        private UInt32 seed;
        private UInt32[] table;
        private static UInt32[] defaultTable;

        /// <summary>
        /// Crc32
        /// </summary>
        public Crc32()
        {
            table = InitializeTable(DefaultPolynomial);
            seed = DefaultSeed;
            Initialize();
        }

        /// <summary>
        /// Crc32
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="seed"></param>
        public Crc32(UInt32 polynomial, UInt32 seed)
        {
            table = InitializeTable(polynomial);
            this.seed = seed;
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            hash = seed;
        }

        /// <summary>
        /// Hash Core
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        protected override void HashCore(byte[] buffer, int start, int length)
        {
            hash = CalculateHash(table, hash, buffer, start, length);
        }

        /// <summary>
        /// Hash Final
        /// </summary>
        /// <returns></returns>
        protected override byte[] HashFinal()
        {
            byte[] hashBuffer = UInt32ToBigEndianBytes(~hash);
            this.HashValue = hashBuffer;
            return hashBuffer;
        }

        /// <summary>
        /// Compute
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static UInt32 Compute(byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Compute
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static UInt32 Compute(UInt32 seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(DefaultPolynomial), seed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Compute
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="seed"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        private static UInt32[] InitializeTable(UInt32 polynomial)
        {
            if (polynomial == DefaultPolynomial && defaultTable != null)
            {
                return defaultTable;
            }
            UInt32[] createTable = new UInt32[256];
            for (int i = 0; i < 256; i++)
            {
                UInt32 entry = (UInt32)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry = entry >> 1;
                }
                createTable[i] = entry;
            }
            if (polynomial == DefaultPolynomial)
            {
                defaultTable = createTable;
            }
            return createTable;
        }

        private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size)
        {
            UInt32 crc = seed;
            for (int i = start; i < size; i++)
            {
                unchecked
                {
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                }
            }
            return crc;
        }

        private byte[] UInt32ToBigEndianBytes(UInt32 x)
        {
            return new byte[] { (byte)((x >> 24) & 0xff), (byte)((x >> 16) & 0xff), (byte)((x >> 8) & 0xff), (byte)(x & 0xff) };
        }
    }
}
