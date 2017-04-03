﻿using HelloWord.Infrastructure;
using HelloWord.TVL.L;

namespace HelloWord.TVL.V
{
    public class ValLength : INumber
    {
        private readonly IBinary _berLen;
        public ValLength(string berLen) : this(new BinaryHex(berLen))
        {}
        public ValLength(IBinary berLen)
        {
            _berLen = berLen;
        }
        public int Value()
        {
            var lenHex = new BytesCount(_berLen).Is(1) 
                                      ? (IBinary)new ShortLen(_berLen) 
                                      : new LongLen(_berLen);
            return new Hex(lenHex)
                        .ToInt();
        }
    }
}
