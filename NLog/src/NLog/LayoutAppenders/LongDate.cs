// 
// Copyright (c) 2004 Jaroslaw Kowalski <jkowalski@users.sourceforge.net>
// 
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of the Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Text;
using System.Globalization;

namespace NLog.LayoutAppenders
{
    [LayoutAppender("longdate")]
    public class LongDateLayoutAppender: LayoutAppender
    {
        protected internal override int GetEstimatedBufferSize(LogEventInfo ev)
        {
            return 24;
        }

        private void Append2DigitsZeroPadded(StringBuilder builder, int number)
        {
            builder.Append((char)((number / 10) + '0'));
            builder.Append((char)((number % 10) + '0'));
        }

        private void Append4DigitsZeroPadded(StringBuilder builder, int number)
        {
            builder.Append((char)((number / 1000 % 10) + '0'));
            builder.Append((char)((number / 100 % 10) + '0'));
            builder.Append((char)((number / 10 % 10) + '0'));
            builder.Append((char)((number / 1 % 10) + '0'));
        }

        protected internal override void Append(StringBuilder builder, LogEventInfo ev)
        {
            if (NeedPadding())
            {
                builder.Append(ApplyPadding(ev.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo)));
            }
            else
            {
                DateTime dt = ev.TimeStamp;

                builder.Append(dt.Year);
                builder.Append('-');
                Append2DigitsZeroPadded(builder, dt.Month);
                builder.Append('-');
                Append2DigitsZeroPadded(builder, dt.Day);
                builder.Append(' ');
                Append2DigitsZeroPadded(builder, dt.Hour);
                builder.Append(':');
                Append2DigitsZeroPadded(builder, dt.Minute);
                builder.Append(':');
                Append2DigitsZeroPadded(builder, dt.Second);
                builder.Append('.');
                Append4DigitsZeroPadded(builder, (int)(dt.Ticks % 10000000) / 1000);
            }
        }
    }
}