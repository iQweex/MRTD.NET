﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWord.Commands;
using HelloWord.Infrastructure;
using HelloWord.ISO7816.ResponseAPDU.Body;
using HelloWord.SecureMessaging;
using HelloWord.SmartCard;
using PCSC;

namespace HelloWord.DataGroups
{
    public class COM : IBinary
    {
        private readonly IReader _reader;
        private readonly IBinary _kSenc;
        private readonly IBinary _kSmac;
        private readonly IBinary _ssc;

        public COM(
                IBinary kSenc,
                IBinary kSmac,
                IBinary ssc,
                IReader reader
            )
        {
            _reader = reader;
            _kSenc = kSenc;
            _kSmac = kSmac;
            _ssc = ssc;
        }
        public byte[] Bytes()
        {
            //return 
                new VerifiedResponseApdu(
                    new ResponseApduData(
                        new CachedBinary(
                            new ExecutedCommandApdu(
                                new DO87ProtectedCommandApdu(
                                    new SelectEFCOMApplicationCommand(),
                                    _kSenc,
                                    _kSmac,
                                    new IncrementedSSC(
                                        _ssc
                                    )
                                ),
                                _reader
                            )
                        )
                    ),
                    new IncrementedSSC(
                        new IncrementedSSC(
                            _ssc
                        )
                    ),
                    _kSmac
                ).Bytes();

            return new ResponseApduData(
                        new CachedBinary(
                            new ExecutedCommandApdu(
                                new DO97ProtectedCommandApdu(
                                    new ReadBinaryCommand(4), 
                                    _kSenc,
                                    _kSmac,
                                    new IncrementedSSC(
                                        new IncrementedSSC(
                                            new IncrementedSSC(
                                                _ssc
                                            )
                                        )
                                    )
                                ),
                                _reader
                            )
                        )
                    ).Bytes();
        }
    }
}