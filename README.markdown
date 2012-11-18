# LicenseKey2 
Is a simple update to Don Sweitzer [License Key Generation](http://www.codeproject.com/Articles/11012/License-Key-Generation)

It consistes of a simplified Factgory to make generating key’s programticlly eaiser as well as a cleanup in the code base to .Net 4

Usage guidelines:- 

'        
        public class TestData : ITokenizable
        {
            [Token(''v'', 8)]
            public string V { get; set; }

            [Token('p', 4)]
            public string P { get; set; }

            [Token('w', 4)]
            public string W { get; set; }

            [Token('s', 8)]
            public string S { get; set; }

            [Token('c', 8)]
            public string C { get; set; }

            [Token('r', 4)]
            public string R { get; set; }
        }
'

# Important Notice

Please don’t consider the use of this code to be secure, you will also require addtional layers of security to protect your programmes.