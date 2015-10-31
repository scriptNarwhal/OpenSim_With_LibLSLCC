/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using LibLSLCC.CodeValidator.Enums;
using LibLSLCC.LibraryData.Reflection;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.ScriptEngine.Interfaces;
using OpenSim.Region.ScriptEngine.Shared.Api.Interfaces;
using integer = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLInteger;
using vector = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Vector3;
using rotation = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Quaternion;
using key = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLString;

using LSL_Float = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLFloat;
using LSL_Integer = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLInteger;
using LSL_Key = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLString;
using LSL_List = OpenSim.Region.ScriptEngine.Shared.LSL_Types.list;
using LSL_Rotation = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Quaternion;
using LSL_String = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLString;
using LSL_Vector = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Vector3;

namespace OpenSim.Region.ScriptEngine.Shared.ScriptBase
{
    public partial class ScriptBaseClass : MarshalByRefObject
    {
        public IMOD_Api m_MOD_Functions;

        public void ApiTypeMOD(IScriptApi api)
        {
            if (!(api is IMOD_Api))
                return;

            m_MOD_Functions = (IMOD_Api)api;
        }


        //[LSLParam(LSLType.Void)] only works on variadic parameters, it specifies 'any'
        //you can use the LSLParam attributes on other types of variadic parameters as well, the fact that they are variadic
        //will be detected by the serializer.  LibLSLCC implements type checked variadic parameters in its compiler.


        [LSLFunction(LSLType.Void)]
        public void modInvokeN([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {

            m_MOD_Functions.modInvokeN(fname, parms);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String modInvokeS([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {
            return m_MOD_Functions.modInvokeS(fname, parms);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer modInvokeI([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {
            return m_MOD_Functions.modInvokeI(fname, parms);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float modInvokeF([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {
            return m_MOD_Functions.modInvokeF(fname, parms);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key modInvokeK([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {
            return m_MOD_Functions.modInvokeK(fname, parms);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector modInvokeV([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {
            return m_MOD_Functions.modInvokeV(fname, parms);
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation modInvokeR([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {
            return m_MOD_Functions.modInvokeR(fname, parms);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List modInvokeL([LSLParam(LSLType.String)] string fname, [LSLParam(LSLType.Void)] params object[] parms)
        {
            return m_MOD_Functions.modInvokeL(fname, parms);
        }

        [LSLFunction(LSLType.String)]
        public string modSendCommand([LSLParam(LSLType.String)] string module, [LSLParam(LSLType.String)] string command, [LSLParam(LSLType.String)] string k)
        {
            return m_MOD_Functions.modSendCommand(module, command, k);
        }
    }
}
