﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWord.Cryptography.RandomKeys
{
    public class RNDic : IBinary
    {
        private readonly byte[] _rndInc;
        public RNDic(byte[] rndInc)
        {
            this._rndInc = rndInc;
        }

        public byte[] Bytes()
        {
            //return new BinaryHex("4608F91988702212").Binary();
            return _rndInc;
        }
    }
}
