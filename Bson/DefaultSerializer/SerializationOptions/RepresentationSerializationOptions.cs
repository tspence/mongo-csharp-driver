﻿/* Copyright 2010-2011 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;

using MongoDB.Bson.IO;

namespace MongoDB.Bson.Serialization {
    public class RepresentationSerializationOptions : IBsonSerializationOptions {
        #region private fields
        private BsonType representation;
        private bool allowOverflow;
        private bool allowTruncation;
        #endregion

        #region constructors
        public RepresentationSerializationOptions(
            BsonType representation
        ) {
            this.representation = representation;
        }

        public RepresentationSerializationOptions(
            BsonType representation,
            bool allowOverflow,
            bool allowTruncation
        ) {
            this.representation = representation;
            this.allowOverflow = allowOverflow;
            this.allowTruncation = allowTruncation;
        }
        #endregion

        #region public properties
        public BsonType Representation {
            get { return representation; }
        }

        public bool AllowOverflow {
            get { return allowOverflow; }
        }

        public bool AllowTruncation {
            get { return allowTruncation; }
        }
        #endregion

        #region public methods
        public double ToDouble(
            double value
        ) {
            return value;
        }

        public double ToDouble(
            float value
        ) {
            if (value == float.MinValue) {
                return double.MinValue;
            } else if (value == float.MaxValue) {
                return double.MaxValue;
            } else if (float.IsNegativeInfinity(value)) {
                return double.NegativeInfinity;
            } else if (float.IsPositiveInfinity(value)) {
                return double.PositiveInfinity;
            } else if (float.IsNaN(value)) {
                return double.NaN;
            }
            return value;
        }

        public double ToDouble(
            int value
        ) {
            return value;
        }

        public double ToDouble(
            long value
        ) {
            if (!allowTruncation) {
                if (value != (long) (double) value) {
                    throw new OverflowException();
                }
            }
            return value;
        }

        public double ToDouble(
            short value
        ) {
            return value;
        }

        public double ToDouble(
            uint value
        ) {
            return value;
        }

        public double ToDouble(
            ulong value
        ) {
            if (!allowTruncation) {
                if (value != (ulong) (double) value) {
                    throw new OverflowException();
                }
            }
            return value;
        }

        public double ToDouble(
            ushort value
        ) {
            return value;
        }

        public short ToInt16(
            double value
        ) {
            if (!allowOverflow) {
                if (value < short.MinValue || value > short.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (double) (short) value) {
                    throw new OverflowException();
                }
            }
            return (short) value;
        }

        public short ToInt16(
            int value
        ) {
            if (!allowOverflow) {
                if (value < short.MinValue || value > short.MaxValue) {
                    throw new OverflowException();
                }
            }
            return (short) value;
        }

        public short ToInt16(
            long value
        ) {
            if (!allowOverflow) {
                if (value < short.MinValue || value > short.MaxValue) {
                    throw new OverflowException();
                }
            }
            return (short) value;
        }

        public int ToInt32(
            double value
        ) {
            if (!allowOverflow) {
                if (value < int.MinValue || value > int.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (double) (int) value) {
                    throw new OverflowException();
                }
            }
            return (int) value;
        }

        public int ToInt32(
            float value
        ) {
            if (!allowOverflow) {
                if (value < int.MinValue || value > int.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (float) (int) value) {
                    throw new OverflowException();
                }
            }
            return (int) value;
        }

        public int ToInt32(
            int value
        ) {
            return value;
        }

        public int ToInt32(
            long value
        ) {
            if (!allowOverflow) {
                if (value < int.MinValue || value > int.MaxValue) {
                    throw new OverflowException();
                }
            }
            return (int) value;
        }

        public int ToInt32(
            short value
        ) {
            return value;
        }

        public int ToInt32(
            uint value
        ) {
            if (!allowOverflow) {
                if (value > (uint) int.MaxValue) {
                    throw new OverflowException();
                }
            }
            return (int) value;
        }

        public int ToInt32(
            ulong value
        ) {
            if (!allowOverflow) {
                if (value > (ulong) int.MaxValue) {
                    throw new OverflowException();
                }
            }
            return (int) value;
        }

        public int ToInt32(
            ushort value
        ) {
            return value;
        }

        public long ToInt64(
            double value
        ) {
            if (!allowOverflow) {
                if (value < long.MinValue || value > long.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (double) (long) value) {
                    throw new OverflowException();
                }
            }
            return (long) value;
        }

        public long ToInt64(
            float value
        ) {
            if (!allowOverflow) {
                if (value < long.MinValue || value > long.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (float) (long) value) {
                    throw new OverflowException();
                }
            }
            return (long) value;
        }

        public long ToInt64(
            int value
        ) {
            return value;
        }

        public long ToInt64(
            long value
        ) {
            return value;
        }

        public long ToInt64(
            short value
        ) {
            return value;
        }

        public long ToInt64(
            uint value
        ) {
            return (long) value;
        }

        public long ToInt64(
            ulong value
        ) {
            if (!allowOverflow) {
                if (value > (ulong) long.MaxValue) {
                    throw new OverflowException();
                }
            }
            return (long) value;
        }

        public long ToInt64(
            ushort value
        ) {
            return value;
        }

        public float ToSingle(
            double value
        ) {
            if (value == double.MinValue) {
                return float.MinValue;
            } else if (value == double.MaxValue) {
                return float.MaxValue;
            } else if (double.IsNegativeInfinity(value)) {
                return float.NegativeInfinity;
            } else if (double.IsPositiveInfinity(value)) {
                return float.PositiveInfinity;
            } else if (double.IsNaN(value)) {
                return float.NaN;
            }
            if (!allowOverflow) {
                if (value < float.MinValue || value > float.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (double) (float) value) {
                    throw new OverflowException();
                }
            }
            return (float) value;
        }

        public float ToSingle(
            int value
        ) {
            if (!allowTruncation) {
                if (value != (int) (float) value) {
                    throw new OverflowException();
                }
            }
            return value;
        }

        public float ToSingle(
            long value
        ) {
            if (!allowTruncation) {
                if (value != (long) (float) value) {
                    throw new OverflowException();
                }
            }
            return value;
        }

        public ushort ToUInt16(
            double value
        ) {
            if (!allowOverflow) {
                if (value < ushort.MinValue || value > ushort.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (double) (ushort) value) {
                    throw new OverflowException();
                }
            }
            return (ushort) value;
        }

        public ushort ToUInt16(
            int value
        ) {
            if (!allowOverflow) {
                if (value < ushort.MinValue || value > ushort.MaxValue) {
                    throw new OverflowException();
                }
            }
            return (ushort) value;
        }

        public ushort ToUInt16(
            long value
        ) {
            if (!allowOverflow) {
                if (value < ushort.MinValue) {
                    throw new OverflowException();
                }
            }
            return (ushort) value;
        }

        public uint ToUInt32(
            double value
        ) {
            if (!allowOverflow) {
                if (value < uint.MinValue || value > uint.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (double) (uint) value) {
                    throw new OverflowException();
                }
            }
            return (uint) value;
        }

        public uint ToUInt32(
            int value
        ) {
            if (!allowOverflow) {
                if (value < uint.MinValue) {
                    throw new OverflowException();
                }
            }
            return (uint) value;
        }

        public uint ToUInt32(
            long value
        ) {
            if (!allowOverflow) {
                if (value < ushort.MinValue) {
                    throw new OverflowException();
                }
            }
            return (uint) value;
        }

        public ulong ToUInt64(
            double value
        ) {
            if (!allowOverflow) {
                if (value < ulong.MinValue || value > ulong.MaxValue) {
                    throw new OverflowException();
                }
            }
            if (!allowTruncation) {
                if (value != (double) (ulong) value) {
                    throw new OverflowException();
                }
            }
            return (ulong) value;
        }

        public ulong ToUInt64(
            int value
        ) {
            if (!allowOverflow) {
                if (value < (int) ulong.MinValue) {
                    throw new OverflowException();
                }
            }
            return (ulong) value;
        }

        public ulong ToUInt64(
            long value
        ) {
            if (!allowOverflow) {
                if (value < (int) ulong.MinValue) {
                    throw new OverflowException();
                }
            }
            return (ulong) value;
        }
        #endregion
    }
}
