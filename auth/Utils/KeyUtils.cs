using Chilkat;
using Consul;
using System;
using System.Collections.Generic;
using System.Text;

namespace auth.Utils
{
    public static class KeyUtils
    {
        private static PrivateKey _privateKey;
        private static String _publicKey;

        public static Boolean _isCreated = false;

        public static PrivateKey GetPrivateKey()
        {
            return _privateKey;
        }

        public static String GetPublicKey()
        {
            return _publicKey;
        }

        public static void CreateKey() {
            Global glob = new Global();
            glob.UnlockBundle("Anything for 30-day trial");

            SshKey key = new SshKey();
            Rsa rsaKey = new Rsa();
            rsaKey.GenerateKey(1024);
            _privateKey = rsaKey.ExportPrivateKeyObj();
            Console.WriteLine("Private Key: " + rsaKey.ExportPrivateKey());
            _publicKey = rsaKey.ExportPublicKey();
            Console.WriteLine("Public Key: " + _publicKey);
            using (ConsulClient consulClient = new ConsulClient())
            {
                var putPair = new KVPair("publickey")
                {
                    Value = Encoding.UTF8.GetBytes(_publicKey)
                };
                var putAttempt = consulClient.KV.Put(putPair);
            }
        }

    }
}
