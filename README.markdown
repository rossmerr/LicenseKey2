# LicenseKey2 
Is a simple update to Don Sweitzer [License Key Generation](http://www.codeproject.com/Articles/11012/License-Key-Generation)

It consistes of a simplified factory to make generating key’s programticlly eaiser as well as a cleanup in the code base to .Net 4.5

Usage guidelines:- 

Your class you want to use as a key

        public class TestData : ITokenizable
        {
            [Token('v', 8)]
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


The template

        var template = "vvvvvvvvppppxxxxxxxx-wwwwxxxxxxxxxxxxxxxx-ssssssssxxxxxxxxxxxx-xxxxxxxxxxxxcccccccccccc-xxxxxxxxxxxxxxxxrrrr";

Your class instances

        var test = new TestData
                   {
                       V = "34",
                       P = "6",
                       W = "8",
                       S = "AB",
                       C = "RSd",
                       R = "3"
                   };

Generating the key

        var gen = new KeyFactory(template);
        var gkey = gen.Generate(test);
        var finalkey = gkey.CreateKey();
        
Giving you the following key: 34600-80002-AB000-001RSd-00003

# Important Notice

Please don’t consider the use of this code to be secure, you will also require addtional layers of security to protect your programmes.