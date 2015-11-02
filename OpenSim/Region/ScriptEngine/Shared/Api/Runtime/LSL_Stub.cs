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
using OpenSim.Region.ScriptEngine.Interfaces;
using OpenSim.Region.ScriptEngine.Shared.Api.Interfaces;
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
        public ILSL_Api m_LSL_Functions;

        public void ApiTypeLSL(IScriptApi api)
        {
            if (!(api is ILSL_Api))
                return;

            m_LSL_Functions = (ILSL_Api) api;
        }


        public void state(string newState)
        {
            m_LSL_Functions.state(newState);
        }

        //
        // Script functions
        //
        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llAbs([LSLParam(LSLType.Integer)] int i)
        {
            return m_LSL_Functions.llAbs(i);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llAcos([LSLParam(LSLType.Float)] double val)
        {
            return m_LSL_Functions.llAcos(val);
        }

        [LSLFunction(LSLType.Void)]
        public void llAddToLandBanList([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.Float)] double hours)
        {
            m_LSL_Functions.llAddToLandBanList(avatar, hours);
        }

        [LSLFunction(LSLType.Void)]
        public void llAddToLandPassList([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.Float)] double hours)
        {
            m_LSL_Functions.llAddToLandPassList(avatar, hours);
        }

        [LSLFunction(LSLType.Void)]
        public void llAdjustSoundVolume([LSLParam(LSLType.Float)] double volume)
        {
            m_LSL_Functions.llAdjustSoundVolume(volume);
        }

        [LSLFunction(LSLType.Void)]
        public void llAllowInventoryDrop([LSLParam(LSLType.Integer)] int add)
        {
            m_LSL_Functions.llAllowInventoryDrop(add);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llAngleBetween([LSLParam(LSLType.Rotation)] LSL_Rotation a,
            [LSLParam(LSLType.Rotation)] LSL_Rotation b)
        {
            return m_LSL_Functions.llAngleBetween(a, b);
        }

        [LSLFunction(LSLType.Void)]
        public void llApplyImpulse([LSLParam(LSLType.Vector)] LSL_Vector force, [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llApplyImpulse(force, local);
        }

        [LSLFunction(LSLType.Void)]
        public void llApplyRotationalImpulse([LSLParam(LSLType.Vector)] LSL_Vector force,
            [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llApplyRotationalImpulse(force, local);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llAsin([LSLParam(LSLType.Float)] double val)
        {
            return m_LSL_Functions.llAsin(val);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llAtan2([LSLParam(LSLType.Float)] double x, [LSLParam(LSLType.Float)] double y)
        {
            return m_LSL_Functions.llAtan2(x, y);
        }

        [LSLFunction(LSLType.Void)]
        public void llAttachToAvatar([LSLParam(LSLType.Integer)] int attachment)
        {
            m_LSL_Functions.llAttachToAvatar(attachment);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llAvatarOnSitTarget()
        {
            return m_LSL_Functions.llAvatarOnSitTarget();
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llAvatarOnLinkSitTarget([LSLParam(LSLType.Integer)] int linknum)
        {
            return m_LSL_Functions.llAvatarOnLinkSitTarget(linknum);
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llAxes2Rot([LSLParam(LSLType.Vector)] LSL_Vector fwd,
            [LSLParam(LSLType.Vector)] LSL_Vector left, [LSLParam(LSLType.Vector)] LSL_Vector up)
        {
            return m_LSL_Functions.llAxes2Rot(fwd, left, up);
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llAxisAngle2Rot([LSLParam(LSLType.Vector)] LSL_Vector axis,
            [LSLParam(LSLType.Float)] double angle)
        {
            return m_LSL_Functions.llAxisAngle2Rot(axis, angle);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llBase64ToInteger([LSLParam(LSLType.String)] string str)
        {
            return m_LSL_Functions.llBase64ToInteger(str);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llBase64ToString([LSLParam(LSLType.String)] string str)
        {
            return m_LSL_Functions.llBase64ToString(str);
        }

        [LSLFunction(LSLType.Void)]
        public void llBreakAllLinks()
        {
            m_LSL_Functions.llBreakAllLinks();
        }

        [LSLFunction(LSLType.Void)]
        public void llBreakLink([LSLParam(LSLType.Integer)] int linknum)
        {
            m_LSL_Functions.llBreakLink(linknum);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llCeil([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llCeil(f);
        }

        [LSLFunction(LSLType.Void)]
        public void llClearCameraParams()
        {
            m_LSL_Functions.llClearCameraParams();
        }

        [LSLFunction(LSLType.Void)]
        public void llCloseRemoteDataChannel([LSLParam(LSLType.String)] string channel)
        {
            m_LSL_Functions.llCloseRemoteDataChannel(channel);
        }

        [LSLFunction(LSLType.Float, Deprecated = true)]
        public LSL_Float llCloud([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            return m_LSL_Functions.llCloud(offset);
        }

        [LSLFunction(LSLType.Void)]
        public void llCollisionFilter([LSLParam(LSLType.String)] string name, [LSLParam(LSLType.String)] string id,
            [LSLParam(LSLType.Integer)] int accept)
        {
            m_LSL_Functions.llCollisionFilter(name, id, accept);
        }

        [LSLFunction(LSLType.Void)]
        public void llCollisionSound([LSLParam(LSLType.String)] string impact_sound,
            [LSLParam(LSLType.Float)] double impact_volume)
        {
            m_LSL_Functions.llCollisionSound(impact_sound, impact_volume);
        }

        [LSLFunction(LSLType.Void)]
        public void llCollisionSprite([LSLParam(LSLType.String)] string impact_sprite)
        {
            m_LSL_Functions.llCollisionSprite(impact_sprite);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llCos([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llCos(f);
        }

        [LSLFunction(LSLType.Void)]
        public void llCreateLink([LSLParam(LSLType.String)] string target, [LSLParam(LSLType.Integer)] int parent)
        {
            m_LSL_Functions.llCreateLink(target, parent);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llCSV2List([LSLParam(LSLType.String)] string src)
        {
            return m_LSL_Functions.llCSV2List(src);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llDeleteSubList([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int start,
            [LSLParam(LSLType.Integer)] int end)
        {
            return m_LSL_Functions.llDeleteSubList(src, start, end);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llDeleteSubString([LSLParam(LSLType.String)] string src, [LSLParam(LSLType.Integer)] int start,
            [LSLParam(LSLType.Integer)] int end)
        {
            return m_LSL_Functions.llDeleteSubString(src, start, end);
        }

        [LSLFunction(LSLType.Void)]
        public void llDetachFromAvatar()
        {
            m_LSL_Functions.llDetachFromAvatar();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedGrab([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedGrab(number);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llDetectedGroup([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedGroup(number);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llDetectedKey([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedKey(number);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llDetectedLinkNumber([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedLinkNumber(number);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llDetectedName([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedName(number);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llDetectedOwner([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedOwner(number);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedPos([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedPos(number);
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llDetectedRot([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedRot(number);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llDetectedType([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedType(number);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedTouchBinormal([LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llDetectedTouchBinormal(index);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llDetectedTouchFace([LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llDetectedTouchFace(index);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedTouchNormal([LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llDetectedTouchNormal(index);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedTouchPos([LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llDetectedTouchPos(index);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedTouchST([LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llDetectedTouchST(index);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedTouchUV([LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llDetectedTouchUV(index);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llDetectedVel([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llDetectedVel(number);
        }

        [LSLFunction(LSLType.Void)]
        public void llDialog([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.String)] string message,
            [LSLParam(LSLType.List)] LSL_List buttons, [LSLParam(LSLType.Integer)] int chat_channel)
        {
            m_LSL_Functions.llDialog(avatar, message, buttons, chat_channel);
        }

        [LSLFunction(LSLType.Void)]
        public void llDie()
        {
            m_LSL_Functions.llDie();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llDumpList2String([LSLParam(LSLType.List)] LSL_List src,
            [LSLParam(LSLType.String)] string seperator)
        {
            return m_LSL_Functions.llDumpList2String(src, seperator);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llEdgeOfWorld([LSLParam(LSLType.Vector)] LSL_Vector pos,
            [LSLParam(LSLType.Vector)] LSL_Vector dir)
        {
            return m_LSL_Functions.llEdgeOfWorld(pos, dir);
        }

        [LSLFunction(LSLType.Void)]
        public void llEjectFromLand([LSLParam(LSLType.String)] string pest)
        {
            m_LSL_Functions.llEjectFromLand(pest);
        }

        [LSLFunction(LSLType.Void)]
        public void llEmail([LSLParam(LSLType.String)] string address, [LSLParam(LSLType.String)] string subject,
            [LSLParam(LSLType.String)] string message)
        {
            m_LSL_Functions.llEmail(address, subject, message);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llEscapeURL([LSLParam(LSLType.String)] string url)
        {
            return m_LSL_Functions.llEscapeURL(url);
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llEuler2Rot([LSLParam(LSLType.Vector)] LSL_Vector v)
        {
            return m_LSL_Functions.llEuler2Rot(v);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llFabs([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llFabs(f);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llFloor([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llFloor(f);
        }

        [LSLFunction(LSLType.Void)]
        public void llForceMouselook([LSLParam(LSLType.Integer)] int mouselook)
        {
            m_LSL_Functions.llForceMouselook(mouselook);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llFrand([LSLParam(LSLType.Float)] double mag)
        {
            return m_LSL_Functions.llFrand(mag);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGenerateKey()
        {
            return m_LSL_Functions.llGenerateKey();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetAccel()
        {
            return m_LSL_Functions.llGetAccel();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetAgentInfo([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetAgentInfo(id);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetAgentLanguage([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetAgentLanguage(id);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetAgentList([LSLParam(LSLType.Integer)] LSL_Integer scope,
            [LSLParam(LSLType.List)] LSL_List options)
        {
            return m_LSL_Functions.llGetAgentList(scope, options);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetAgentSize([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetAgentSize(id);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetAlpha([LSLParam(LSLType.Integer)] int face)
        {
            return m_LSL_Functions.llGetAlpha(face);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetAndResetTime()
        {
            return m_LSL_Functions.llGetAndResetTime();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetAnimation([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetAnimation(id);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetAnimationList([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetAnimationList(id);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetAttached()
        {
            return m_LSL_Functions.llGetAttached();
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetBoundingBox([LSLParam(LSLType.String)] string obj)
        {
            return m_LSL_Functions.llGetBoundingBox(obj);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetCameraPos()
        {
            return m_LSL_Functions.llGetCameraPos();
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llGetCameraRot()
        {
            return m_LSL_Functions.llGetCameraRot();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetCenterOfMass()
        {
            return m_LSL_Functions.llGetCenterOfMass();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetColor([LSLParam(LSLType.Integer)] int face)
        {
            return m_LSL_Functions.llGetColor(face);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetCreator()
        {
            return m_LSL_Functions.llGetCreator();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetDate()
        {
            return m_LSL_Functions.llGetDate();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetEnergy()
        {
            return m_LSL_Functions.llGetEnergy();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetEnv([LSLParam(LSLType.String)] LSL_String name)
        {
            return m_LSL_Functions.llGetEnv(name);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetForce()
        {
            return m_LSL_Functions.llGetForce();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetFreeMemory()
        {
            return m_LSL_Functions.llGetFreeMemory();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetFreeURLs()
        {
            return m_LSL_Functions.llGetFreeURLs();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetGeometricCenter()
        {
            return m_LSL_Functions.llGetGeometricCenter();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetGMTclock()
        {
            return m_LSL_Functions.llGetGMTclock();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetHTTPHeader([LSLParam(LSLType.Key)] LSL_Key request_id,
            [LSLParam(LSLType.String)] string header)
        {
            return m_LSL_Functions.llGetHTTPHeader(request_id, header);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetInventoryCreator([LSLParam(LSLType.String)] string item)
        {
            return m_LSL_Functions.llGetInventoryCreator(item);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetInventoryKey([LSLParam(LSLType.String)] string name)
        {
            return m_LSL_Functions.llGetInventoryKey(name);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetInventoryName([LSLParam(LSLType.Integer)] int type,
            [LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llGetInventoryName(type, number);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetInventoryNumber([LSLParam(LSLType.Integer)] int type)
        {
            return m_LSL_Functions.llGetInventoryNumber(type);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetInventoryPermMask([LSLParam(LSLType.String)] string item,
            [LSLParam(LSLType.Integer)] int mask)
        {
            return m_LSL_Functions.llGetInventoryPermMask(item, mask);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetInventoryType([LSLParam(LSLType.String)] string name)
        {
            return m_LSL_Functions.llGetInventoryType(name);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetKey()
        {
            return m_LSL_Functions.llGetKey();
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetLandOwnerAt([LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            return m_LSL_Functions.llGetLandOwnerAt(pos);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetLinkKey([LSLParam(LSLType.Integer)] int linknum)
        {
            return m_LSL_Functions.llGetLinkKey(linknum);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetLinkName([LSLParam(LSLType.Integer)] int linknum)
        {
            return m_LSL_Functions.llGetLinkName(linknum);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetLinkNumber()
        {
            return m_LSL_Functions.llGetLinkNumber();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetLinkNumberOfSides([LSLParam(LSLType.Integer)] int link)
        {
            return m_LSL_Functions.llGetLinkNumberOfSides(link);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetKeyframedMotion([LSLParam(LSLType.List)] LSL_List frames,
            [LSLParam(LSLType.List)] LSL_List options)
        {
            m_LSL_Functions.llSetKeyframedMotion(frames, options);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetListEntryType([LSLParam(LSLType.List)] LSL_List src,
            [LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llGetListEntryType(src, index);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetListLength([LSLParam(LSLType.List)] LSL_List src)
        {
            return m_LSL_Functions.llGetListLength(src);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetLocalPos()
        {
            return m_LSL_Functions.llGetLocalPos();
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llGetLocalRot()
        {
            return m_LSL_Functions.llGetLocalRot();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetMass()
        {
            return m_LSL_Functions.llGetMass();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetMassMKS()
        {
            return m_LSL_Functions.llGetMassMKS();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetMemoryLimit()
        {
            return m_LSL_Functions.llGetMemoryLimit();
        }

        [LSLFunction(LSLType.Void)]
        public void llGetNextEmail([LSLParam(LSLType.String)] string address, [LSLParam(LSLType.String)] string subject)
        {
            m_LSL_Functions.llGetNextEmail(address, subject);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetNotecardLine([LSLParam(LSLType.String)] string name, [LSLParam(LSLType.Integer)] int line)
        {
            return m_LSL_Functions.llGetNotecardLine(name, line);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetNumberOfNotecardLines([LSLParam(LSLType.String)] string name)
        {
            return m_LSL_Functions.llGetNumberOfNotecardLines(name);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetNumberOfPrims()
        {
            return m_LSL_Functions.llGetNumberOfPrims();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetNumberOfSides()
        {
            return m_LSL_Functions.llGetNumberOfSides();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetObjectDesc()
        {
            return m_LSL_Functions.llGetObjectDesc();
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetObjectDetails([LSLParam(LSLType.String)] string id, [LSLParam(LSLType.List)] LSL_List args)
        {
            return m_LSL_Functions.llGetObjectDetails(id, args);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetObjectMass([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetObjectMass(id);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetObjectName()
        {
            return m_LSL_Functions.llGetObjectName();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetObjectPermMask([LSLParam(LSLType.Integer)] int mask)
        {
            return m_LSL_Functions.llGetObjectPermMask(mask);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetObjectPrimCount([LSLParam(LSLType.String)] string object_id)
        {
            return m_LSL_Functions.llGetObjectPrimCount(object_id);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetOmega()
        {
            return m_LSL_Functions.llGetOmega();
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetOwner()
        {
            return m_LSL_Functions.llGetOwner();
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetOwnerKey([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetOwnerKey(id);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetParcelDetails([LSLParam(LSLType.Vector)] LSL_Vector pos,
            [LSLParam(LSLType.List)] LSL_List param)
        {
            return m_LSL_Functions.llGetParcelDetails(pos, param);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetParcelFlags([LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            return m_LSL_Functions.llGetParcelFlags(pos);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetParcelMaxPrims([LSLParam(LSLType.Vector)] LSL_Vector pos,
            [LSLParam(LSLType.Integer)] int sim_wide)
        {
            return m_LSL_Functions.llGetParcelMaxPrims(pos, sim_wide);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetParcelMusicURL()
        {
            return m_LSL_Functions.llGetParcelMusicURL();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetParcelPrimCount([LSLParam(LSLType.Vector)] LSL_Vector pos,
            [LSLParam(LSLType.Integer)] int category, [LSLParam(LSLType.Integer)] int sim_wide)
        {
            return m_LSL_Functions.llGetParcelPrimCount(pos, category, sim_wide);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetParcelPrimOwners([LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            return m_LSL_Functions.llGetParcelPrimOwners(pos);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetPermissions()
        {
            return m_LSL_Functions.llGetPermissions();
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llGetPermissionsKey()
        {
            return m_LSL_Functions.llGetPermissionsKey();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetPos()
        {
            return m_LSL_Functions.llGetPos();
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetPrimitiveParams([LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_LSL_Functions.llGetPrimitiveParams(rules);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetLinkPrimitiveParams([LSLParam(LSLType.Integer)] int linknum,
            [LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_LSL_Functions.llGetLinkPrimitiveParams(linknum, rules);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetRegionAgentCount()
        {
            return m_LSL_Functions.llGetRegionAgentCount();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetRegionCorner()
        {
            return m_LSL_Functions.llGetRegionCorner();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetRegionFlags()
        {
            return m_LSL_Functions.llGetRegionFlags();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetRegionFPS()
        {
            return m_LSL_Functions.llGetRegionFPS();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetRegionName()
        {
            return m_LSL_Functions.llGetRegionName();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetRegionTimeDilation()
        {
            return m_LSL_Functions.llGetRegionTimeDilation();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetRootPosition()
        {
            return m_LSL_Functions.llGetRootPosition();
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llGetRootRotation()
        {
            return m_LSL_Functions.llGetRootRotation();
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llGetRot()
        {
            return m_LSL_Functions.llGetRot();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetScale()
        {
            return m_LSL_Functions.llGetScale();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetScriptName()
        {
            return m_LSL_Functions.llGetScriptName();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetScriptState([LSLParam(LSLType.String)] string name)
        {
            return m_LSL_Functions.llGetScriptState(name);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetSimulatorHostname()
        {
            return m_LSL_Functions.llGetSimulatorHostname();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetSPMaxMemory()
        {
            return m_LSL_Functions.llGetSPMaxMemory();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetStartParameter()
        {
            return m_LSL_Functions.llGetStartParameter();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetStatus([LSLParam(LSLType.Integer)] int status)
        {
            return m_LSL_Functions.llGetStatus(status);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetSubString([LSLParam(LSLType.String)] string src, [LSLParam(LSLType.Integer)] int start,
            [LSLParam(LSLType.Integer)] int end)
        {
            return m_LSL_Functions.llGetSubString(src, start, end);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetSunDirection()
        {
            return m_LSL_Functions.llGetSunDirection();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetTexture([LSLParam(LSLType.Integer)] int face)
        {
            return m_LSL_Functions.llGetTexture(face);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetTextureOffset([LSLParam(LSLType.Integer)] int face)
        {
            return m_LSL_Functions.llGetTextureOffset(face);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetTextureRot([LSLParam(LSLType.Integer)] int side)
        {
            return m_LSL_Functions.llGetTextureRot(side);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetTextureScale([LSLParam(LSLType.Integer)] int side)
        {
            return m_LSL_Functions.llGetTextureScale(side);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetTime()
        {
            return m_LSL_Functions.llGetTime();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetTimeOfDay()
        {
            return m_LSL_Functions.llGetTimeOfDay();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetTimestamp()
        {
            return m_LSL_Functions.llGetTimestamp();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetTorque()
        {
            return m_LSL_Functions.llGetTorque();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetUnixTime()
        {
            return m_LSL_Functions.llGetUnixTime();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llGetUsedMemory()
        {
            return m_LSL_Functions.llGetUsedMemory();
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGetVel()
        {
            return m_LSL_Functions.llGetVel();
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGetWallclock()
        {
            return m_LSL_Functions.llGetWallclock();
        }

        [LSLFunction(LSLType.Void)]
        public void llGiveInventory([LSLParam(LSLType.String)] string destination,
            [LSLParam(LSLType.String)] string inventory)
        {
            m_LSL_Functions.llGiveInventory(destination, inventory);
        }

        [LSLFunction(LSLType.Void)]
        public void llGiveInventoryList([LSLParam(LSLType.String)] string destination,
            [LSLParam(LSLType.String)] string category, [LSLParam(LSLType.List)] LSL_List inventory)
        {
            m_LSL_Functions.llGiveInventoryList(destination, category, inventory);
        }

        [LSLFunction(LSLType.Void)]
        public void llGiveMoney([LSLParam(LSLType.String)] string destination, [LSLParam(LSLType.Integer)] int amount)
        {
            m_LSL_Functions.llGiveMoney(destination, amount);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llTransferLindenDollars([LSLParam(LSLType.String)] string destination,
            [LSLParam(LSLType.Integer)] int amount)
        {
            return m_LSL_Functions.llTransferLindenDollars(destination, amount);
        }

        [LSLFunction(LSLType.Void)]
        public void llGodLikeRezObject([LSLParam(LSLType.String)] string inventory,
            [LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            m_LSL_Functions.llGodLikeRezObject(inventory, pos);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llGround([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            return m_LSL_Functions.llGround(offset);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGroundContour([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            return m_LSL_Functions.llGroundContour(offset);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGroundNormal([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            return m_LSL_Functions.llGroundNormal(offset);
        }

        [LSLFunction(LSLType.Void)]
        public void llGroundRepel([LSLParam(LSLType.Float)] double height, [LSLParam(LSLType.Integer)] int water,
            [LSLParam(LSLType.Float)] double tau)
        {
            m_LSL_Functions.llGroundRepel(height, water, tau);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llGroundSlope([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            return m_LSL_Functions.llGroundSlope(offset);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llHTTPRequest([LSLParam(LSLType.String)] string url,
            [LSLParam(LSLType.List)] LSL_List parameters, [LSLParam(LSLType.String)] string body)
        {
            return m_LSL_Functions.llHTTPRequest(url, parameters, body);
        }

        [LSLFunction(LSLType.Void)]
        public void llHTTPResponse([LSLParam(LSLType.Key)] LSL_Key id, [LSLParam(LSLType.Integer)] int status,
            [LSLParam(LSLType.String)] string body)
        {
            m_LSL_Functions.llHTTPResponse(id, status, body);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llInsertString([LSLParam(LSLType.String)] string dst, [LSLParam(LSLType.Integer)] int position,
            [LSLParam(LSLType.String)] string src)
        {
            return m_LSL_Functions.llInsertString(dst, position, src);
        }

        [LSLFunction(LSLType.Void)]
        public void llInstantMessage([LSLParam(LSLType.String)] string user, [LSLParam(LSLType.String)] string message)
        {
            m_LSL_Functions.llInstantMessage(user, message);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llIntegerToBase64([LSLParam(LSLType.Integer)] int number)
        {
            return m_LSL_Functions.llIntegerToBase64(number);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llKey2Name([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llKey2Name(id);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetUsername([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetUsername(id);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llRequestUsername([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llRequestUsername(id);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llGetDisplayName([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llGetDisplayName(id);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llRequestDisplayName([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llRequestDisplayName(id);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llCastRay([LSLParam(LSLType.Vector)] LSL_Vector start, [LSLParam(LSLType.Vector)] LSL_Vector end,
            [LSLParam(LSLType.List)] LSL_List options)
        {
            return m_LSL_Functions.llCastRay(start, end, options);
        }

        [LSLFunction(LSLType.Void)]
        public void llLinkParticleSystem([LSLParam(LSLType.Integer)] int linknum,
            [LSLParam(LSLType.List)] LSL_List rules)
        {
            m_LSL_Functions.llLinkParticleSystem(linknum, rules);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llList2CSV([LSLParam(LSLType.List)] LSL_List src)
        {
            return m_LSL_Functions.llList2CSV(src);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llList2Float([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llList2Float(src, index);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llList2Integer([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llList2Integer(src, index);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llList2Key([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llList2Key(src, index);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llList2List([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int start,
            [LSLParam(LSLType.Integer)] int end)
        {
            return m_LSL_Functions.llList2List(src, start, end);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llList2ListStrided([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int start,
            [LSLParam(LSLType.Integer)] int end, [LSLParam(LSLType.Integer)] int stride)
        {
            return m_LSL_Functions.llList2ListStrided(src, start, end, stride);
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llList2Rot([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llList2Rot(src, index);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llList2String([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llList2String(src, index);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llList2Vector([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int index)
        {
            return m_LSL_Functions.llList2Vector(src, index);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llListen([LSLParam(LSLType.Integer)] int channelID, [LSLParam(LSLType.String)] string name,
            [LSLParam(LSLType.String)] string ID, [LSLParam(LSLType.String)] string msg)
        {
            return m_LSL_Functions.llListen(channelID, name, ID, msg);
        }

        [LSLFunction(LSLType.Void)]
        public void llListenControl([LSLParam(LSLType.Integer)] int number, [LSLParam(LSLType.Integer)] int active)
        {
            m_LSL_Functions.llListenControl(number, active);
        }

        [LSLFunction(LSLType.Void)]
        public void llListenRemove([LSLParam(LSLType.Integer)] int number)
        {
            m_LSL_Functions.llListenRemove(number);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llListFindList([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.List)] LSL_List test)
        {
            return m_LSL_Functions.llListFindList(src, test);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llListInsertList([LSLParam(LSLType.List)] LSL_List dest, [LSLParam(LSLType.List)] LSL_List src,
            [LSLParam(LSLType.Integer)] int start)
        {
            return m_LSL_Functions.llListInsertList(dest, src, start);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llListRandomize([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int stride)
        {
            return m_LSL_Functions.llListRandomize(src, stride);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llListReplaceList([LSLParam(LSLType.List)] LSL_List dest, [LSLParam(LSLType.List)] LSL_List src,
            [LSLParam(LSLType.Integer)] int start, [LSLParam(LSLType.Integer)] int end)
        {
            return m_LSL_Functions.llListReplaceList(dest, src, start, end);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llListSort([LSLParam(LSLType.List)] LSL_List src, [LSLParam(LSLType.Integer)] int stride,
            [LSLParam(LSLType.Integer)] int ascending)
        {
            return m_LSL_Functions.llListSort(src, stride, ascending);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llListStatistics([LSLParam(LSLType.Integer)] int operation,
            [LSLParam(LSLType.List)] LSL_List src)
        {
            return m_LSL_Functions.llListStatistics(operation, src);
        }

        [LSLFunction(LSLType.Void)]
        public void llLoadURL([LSLParam(LSLType.String)] string avatar_id, [LSLParam(LSLType.String)] string message,
            [LSLParam(LSLType.String)] string url)
        {
            m_LSL_Functions.llLoadURL(avatar_id, message, url);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llLog([LSLParam(LSLType.Float)] double val)
        {
            return m_LSL_Functions.llLog(val);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llLog10([LSLParam(LSLType.Float)] double val)
        {
            return m_LSL_Functions.llLog10(val);
        }

        [LSLFunction(LSLType.Void)]
        public void llLookAt([LSLParam(LSLType.Vector)] LSL_Vector target, [LSLParam(LSLType.Float)] double strength,
            [LSLParam(LSLType.Float)] double damping)
        {
            m_LSL_Functions.llLookAt(target, strength, damping);
        }

        [LSLFunction(LSLType.Void)]
        public void llLoopSound([LSLParam(LSLType.String)] string sound, [LSLParam(LSLType.Float)] double volume)
        {
            m_LSL_Functions.llLoopSound(sound, volume);
        }

        [LSLFunction(LSLType.Void)]
        public void llLoopSoundMaster([LSLParam(LSLType.String)] string sound, [LSLParam(LSLType.Float)] double volume)
        {
            m_LSL_Functions.llLoopSoundMaster(sound, volume);
        }

        [LSLFunction(LSLType.Void)]
        public void llLoopSoundSlave([LSLParam(LSLType.String)] string sound, [LSLParam(LSLType.Float)] double volume)
        {
            m_LSL_Functions.llLoopSoundSlave(sound, volume);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llManageEstateAccess([LSLParam(LSLType.Integer)] int action,
            [LSLParam(LSLType.String)] string avatar)
        {
            return m_LSL_Functions.llManageEstateAccess(action, avatar);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llMakeExplosion([LSLParam(LSLType.Integer)] int particles, [LSLParam(LSLType.Float)] double scale,
            [LSLParam(LSLType.Float)] double vel, [LSLParam(LSLType.Float)] double lifetime,
            [LSLParam(LSLType.Float)] double arc, [LSLParam(LSLType.String)] string texture,
            LSL_Vector offset)
        {
            m_LSL_Functions.llMakeExplosion(particles, scale, vel, lifetime, arc, texture, offset);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llMakeFire([LSLParam(LSLType.Integer)] int particles, [LSLParam(LSLType.Float)] double scale,
            [LSLParam(LSLType.Float)] double vel, [LSLParam(LSLType.Float)] double lifetime,
            [LSLParam(LSLType.Float)] double arc, [LSLParam(LSLType.String)] string texture,
            LSL_Vector offset)
        {
            m_LSL_Functions.llMakeFire(particles, scale, vel, lifetime, arc, texture, offset);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llMakeFountain([LSLParam(LSLType.Integer)] int particles, [LSLParam(LSLType.Float)] double scale,
            [LSLParam(LSLType.Float)] double vel, [LSLParam(LSLType.Float)] double lifetime,
            [LSLParam(LSLType.Float)] double arc, [LSLParam(LSLType.Integer)] int bounce,
            string texture, [LSLParam(LSLType.Vector)] LSL_Vector offset, [LSLParam(LSLType.Float)] double bounce_offset)
        {
            m_LSL_Functions.llMakeFountain(particles, scale, vel, lifetime, arc, bounce, texture, offset, bounce_offset);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llMakeSmoke([LSLParam(LSLType.Integer)] int particles, [LSLParam(LSLType.Float)] double scale,
            [LSLParam(LSLType.Float)] double vel, [LSLParam(LSLType.Float)] double lifetime,
            [LSLParam(LSLType.Float)] double arc, [LSLParam(LSLType.String)] string texture,
            LSL_Vector offset)
        {
            m_LSL_Functions.llMakeSmoke(particles, scale, vel, lifetime, arc, texture, offset);
        }

        [LSLFunction(LSLType.Void)]
        public void llMapDestination([LSLParam(LSLType.String)] string simname,
            [LSLParam(LSLType.Vector)] LSL_Vector pos, [LSLParam(LSLType.Vector)] LSL_Vector look_at)
        {
            m_LSL_Functions.llMapDestination(simname, pos, look_at);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llMD5String([LSLParam(LSLType.String)] string src, [LSLParam(LSLType.Integer)] int nonce)
        {
            return m_LSL_Functions.llMD5String(src, nonce);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llSHA1String([LSLParam(LSLType.String)] string src)
        {
            return m_LSL_Functions.llSHA1String(src);
        }

        [LSLFunction(LSLType.Void)]
        public void llMessageLinked([LSLParam(LSLType.Integer)] int linknum, [LSLParam(LSLType.Integer)] int num,
            [LSLParam(LSLType.String)] string str, [LSLParam(LSLType.String)] string id)
        {
            m_LSL_Functions.llMessageLinked(linknum, num, str, id);
        }

        [LSLFunction(LSLType.Void)]
        public void llMinEventDelay([LSLParam(LSLType.Float)] double delay)
        {
            m_LSL_Functions.llMinEventDelay(delay);
        }

        [LSLFunction(LSLType.Void)]
        public void llModifyLand([LSLParam(LSLType.Integer)] int action, [LSLParam(LSLType.Integer)] int brush)
        {
            m_LSL_Functions.llModifyLand(action, brush);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llModPow([LSLParam(LSLType.Integer)] int a, [LSLParam(LSLType.Integer)] int b,
            [LSLParam(LSLType.Integer)] int c)
        {
            return m_LSL_Functions.llModPow(a, b, c);
        }

        [LSLFunction(LSLType.Void)]
        public void llMoveToTarget([LSLParam(LSLType.Vector)] LSL_Vector target, [LSLParam(LSLType.Float)] double tau)
        {
            m_LSL_Functions.llMoveToTarget(target, tau);
        }

        [LSLFunction(LSLType.Void)]
        public void llOffsetTexture([LSLParam(LSLType.Float)] double u, [LSLParam(LSLType.Float)] double v,
            [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llOffsetTexture(u, v, face);
        }

        [LSLFunction(LSLType.Void)]
        public void llOpenRemoteDataChannel()
        {
            m_LSL_Functions.llOpenRemoteDataChannel();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llOverMyLand([LSLParam(LSLType.String)] string id)
        {
            return m_LSL_Functions.llOverMyLand(id);
        }

        [LSLFunction(LSLType.Void)]
        public void llOwnerSay([LSLParam(LSLType.String)] string msg)
        {
            m_LSL_Functions.llOwnerSay(msg);
        }

        [LSLFunction(LSLType.Void)]
        public void llParcelMediaCommandList([LSLParam(LSLType.List)] LSL_List commandList)
        {
            m_LSL_Functions.llParcelMediaCommandList(commandList);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llParcelMediaQuery([LSLParam(LSLType.List)] LSL_List aList)
        {
            return m_LSL_Functions.llParcelMediaQuery(aList);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llParseString2List([LSLParam(LSLType.String)] string str,
            [LSLParam(LSLType.List)] LSL_List separators, [LSLParam(LSLType.List)] LSL_List spacers)
        {
            return m_LSL_Functions.llParseString2List(str, separators, spacers);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llParseStringKeepNulls([LSLParam(LSLType.String)] string src,
            [LSLParam(LSLType.List)] LSL_List seperators, [LSLParam(LSLType.List)] LSL_List spacers)
        {
            return m_LSL_Functions.llParseStringKeepNulls(src, seperators, spacers);
        }

        [LSLFunction(LSLType.Void)]
        public void llParticleSystem([LSLParam(LSLType.List)] LSL_List rules)
        {
            m_LSL_Functions.llParticleSystem(rules);
        }

        [LSLFunction(LSLType.Void)]
        public void llPassCollisions([LSLParam(LSLType.Integer)] int pass)
        {
            m_LSL_Functions.llPassCollisions(pass);
        }

        [LSLFunction(LSLType.Void)]
        public void llPassTouches([LSLParam(LSLType.Integer)] int pass)
        {
            m_LSL_Functions.llPassTouches(pass);
        }

        [LSLFunction(LSLType.Void)]
        public void llPlaySound([LSLParam(LSLType.String)] string sound, [LSLParam(LSLType.Float)] double volume)
        {
            m_LSL_Functions.llPlaySound(sound, volume);
        }

        [LSLFunction(LSLType.Void)]
        public void llPlaySoundSlave([LSLParam(LSLType.String)] string sound, [LSLParam(LSLType.Float)] double volume)
        {
            m_LSL_Functions.llPlaySoundSlave(sound, volume);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llPointAt([LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            m_LSL_Functions.llPointAt(pos);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llPow([LSLParam(LSLType.Float)] double fbase, [LSLParam(LSLType.Float)] double fexponent)
        {
            return m_LSL_Functions.llPow(fbase, fexponent);
        }

        [LSLFunction(LSLType.Void)]
        public void llPreloadSound([LSLParam(LSLType.String)] string sound)
        {
            m_LSL_Functions.llPreloadSound(sound);
        }

        [LSLFunction(LSLType.Void)]
        public void llPushObject([LSLParam(LSLType.String)] string target, [LSLParam(LSLType.Vector)] LSL_Vector impulse,
            [LSLParam(LSLType.Vector)] LSL_Vector ang_impulse, [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llPushObject(target, impulse, ang_impulse, local);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llRefreshPrimURL()
        {
            m_LSL_Functions.llRefreshPrimURL();
        }

        [LSLFunction(LSLType.Void)]
        public void llRegionSay([LSLParam(LSLType.Integer)] int channelID, [LSLParam(LSLType.String)] string text)
        {
            m_LSL_Functions.llRegionSay(channelID, text);
        }

        [LSLFunction(LSLType.Void)]
        public void llRegionSayTo([LSLParam(LSLType.String)] string key, [LSLParam(LSLType.Integer)] int channelID,
            [LSLParam(LSLType.String)] string text)
        {
            m_LSL_Functions.llRegionSayTo(key, channelID, text);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llReleaseCamera([LSLParam(LSLType.String)] string avatar)
        {
            m_LSL_Functions.llReleaseCamera(avatar);
        }

        [LSLFunction(LSLType.Void)]
        public void llReleaseURL([LSLParam(LSLType.String)] string url)
        {
            m_LSL_Functions.llReleaseURL(url);
        }

        [LSLFunction(LSLType.Void)]
        public void llReleaseControls()
        {
            m_LSL_Functions.llReleaseControls();
        }

        [LSLFunction(LSLType.Void)]
        public void llRemoteDataReply([LSLParam(LSLType.String)] string channel,
            [LSLParam(LSLType.String)] string message_id, [LSLParam(LSLType.String)] string sdata,
            [LSLParam(LSLType.Integer)] int idata)
        {
            m_LSL_Functions.llRemoteDataReply(channel, message_id, sdata, idata);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llRemoteDataSetRegion()
        {
            m_LSL_Functions.llRemoteDataSetRegion();
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llRemoteLoadScript([LSLParam(LSLType.String)] string target, [LSLParam(LSLType.String)] string name,
            [LSLParam(LSLType.Integer)] int running, [LSLParam(LSLType.Integer)] int start_param)
        {
            m_LSL_Functions.llRemoteLoadScript(target, name, running, start_param);
        }

        [LSLFunction(LSLType.Void)]
        public void llRemoteLoadScriptPin([LSLParam(LSLType.String)] string target,
            [LSLParam(LSLType.String)] string name, [LSLParam(LSLType.Integer)] int pin,
            [LSLParam(LSLType.Integer)] int running, [LSLParam(LSLType.Integer)] int start_param)
        {
            m_LSL_Functions.llRemoteLoadScriptPin(target, name, pin, running, start_param);
        }

        [LSLFunction(LSLType.Void)]
        public void llRemoveFromLandBanList([LSLParam(LSLType.String)] string avatar)
        {
            m_LSL_Functions.llRemoveFromLandBanList(avatar);
        }

        [LSLFunction(LSLType.Void)]
        public void llRemoveFromLandPassList([LSLParam(LSLType.String)] string avatar)
        {
            m_LSL_Functions.llRemoveFromLandPassList(avatar);
        }

        [LSLFunction(LSLType.Void)]
        public void llRemoveInventory([LSLParam(LSLType.String)] string item)
        {
            m_LSL_Functions.llRemoveInventory(item);
        }

        [LSLFunction(LSLType.Void)]
        public void llRemoveVehicleFlags([LSLParam(LSLType.Integer)] int flags)
        {
            m_LSL_Functions.llRemoveVehicleFlags(flags);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llRequestAgentData([LSLParam(LSLType.String)] string id, [LSLParam(LSLType.Integer)] int data)
        {
            return m_LSL_Functions.llRequestAgentData(id, data);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llRequestInventoryData([LSLParam(LSLType.String)] string name)
        {
            return m_LSL_Functions.llRequestInventoryData(name);
        }

        [LSLFunction(LSLType.Void)]
        public void llRequestPermissions([LSLParam(LSLType.String)] string agent, [LSLParam(LSLType.Integer)] int perm)
        {
            m_LSL_Functions.llRequestPermissions(agent, perm);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llRequestSecureURL()
        {
            return m_LSL_Functions.llRequestSecureURL();
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llRequestSimulatorData([LSLParam(LSLType.String)] string simulator,
            [LSLParam(LSLType.Integer)] int data)
        {
            return m_LSL_Functions.llRequestSimulatorData(simulator, data);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llRequestURL()
        {
            return m_LSL_Functions.llRequestURL();
        }

        [LSLFunction(LSLType.Void)]
        public void llResetLandBanList()
        {
            m_LSL_Functions.llResetLandBanList();
        }

        [LSLFunction(LSLType.Void)]
        public void llResetLandPassList()
        {
            m_LSL_Functions.llResetLandPassList();
        }

        [LSLFunction(LSLType.Void)]
        public void llResetOtherScript([LSLParam(LSLType.String)] string name)
        {
            m_LSL_Functions.llResetOtherScript(name);
        }

        [LSLFunction(LSLType.Void)]
        public void llResetScript()
        {
            m_LSL_Functions.llResetScript();
        }

        [LSLFunction(LSLType.Void)]
        public void llResetTime()
        {
            m_LSL_Functions.llResetTime();
        }

        [LSLFunction(LSLType.Void)]
        public void llRezAtRoot([LSLParam(LSLType.String)] string inventory,
            [LSLParam(LSLType.Vector)] LSL_Vector position, [LSLParam(LSLType.Vector)] LSL_Vector velocity,
            [LSLParam(LSLType.Rotation)] LSL_Rotation rot, [LSLParam(LSLType.Integer)] int param)
        {
            m_LSL_Functions.llRezAtRoot(inventory, position, velocity, rot, param);
        }

        [LSLFunction(LSLType.Void)]
        public void llRezObject([LSLParam(LSLType.String)] string inventory, [LSLParam(LSLType.Vector)] LSL_Vector pos,
            [LSLParam(LSLType.Vector)] LSL_Vector vel, [LSLParam(LSLType.Rotation)] LSL_Rotation rot,
            [LSLParam(LSLType.Integer)] int param)
        {
            m_LSL_Functions.llRezObject(inventory, pos, vel, rot, param);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llRot2Angle([LSLParam(LSLType.Rotation)] LSL_Rotation rot)
        {
            return m_LSL_Functions.llRot2Angle(rot);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llRot2Axis([LSLParam(LSLType.Rotation)] LSL_Rotation rot)
        {
            return m_LSL_Functions.llRot2Axis(rot);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llRot2Euler([LSLParam(LSLType.Rotation)] LSL_Rotation r)
        {
            return m_LSL_Functions.llRot2Euler(r);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llRot2Fwd([LSLParam(LSLType.Rotation)] LSL_Rotation r)
        {
            return m_LSL_Functions.llRot2Fwd(r);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llRot2Left([LSLParam(LSLType.Rotation)] LSL_Rotation r)
        {
            return m_LSL_Functions.llRot2Left(r);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llRot2Up([LSLParam(LSLType.Rotation)] LSL_Rotation r)
        {
            return m_LSL_Functions.llRot2Up(r);
        }

        [LSLFunction(LSLType.Void)]
        public void llRotateTexture([LSLParam(LSLType.Float)] double rotation, [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llRotateTexture(rotation, face);
        }

        [LSLFunction(LSLType.Rotation)]
        public LSL_Rotation llRotBetween([LSLParam(LSLType.Vector)] LSL_Vector start,
            [LSLParam(LSLType.Vector)] LSL_Vector end)
        {
            return m_LSL_Functions.llRotBetween(start, end);
        }

        [LSLFunction(LSLType.Void)]
        public void llRotLookAt([LSLParam(LSLType.Rotation)] LSL_Rotation target,
            [LSLParam(LSLType.Float)] double strength, [LSLParam(LSLType.Float)] double damping)
        {
            m_LSL_Functions.llRotLookAt(target, strength, damping);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llRotTarget([LSLParam(LSLType.Rotation)] LSL_Rotation rot,
            [LSLParam(LSLType.Float)] double error)
        {
            return m_LSL_Functions.llRotTarget(rot, error);
        }

        [LSLFunction(LSLType.Void)]
        public void llRotTargetRemove([LSLParam(LSLType.Integer)] int number)
        {
            m_LSL_Functions.llRotTargetRemove(number);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llRound([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llRound(f);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llSameGroup([LSLParam(LSLType.String)] string agent)
        {
            return m_LSL_Functions.llSameGroup(agent);
        }

        [LSLFunction(LSLType.Void)]
        public void llSay([LSLParam(LSLType.Integer)] int channelID, [LSLParam(LSLType.String)] string text)
        {
            m_LSL_Functions.llSay(channelID, text);
        }

        [LSLFunction(LSLType.Void)]
        public void llScaleTexture([LSLParam(LSLType.Float)] double u, [LSLParam(LSLType.Float)] double v,
            [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llScaleTexture(u, v, face);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llScriptDanger([LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            return m_LSL_Functions.llScriptDanger(pos);
        }

        [LSLFunction(LSLType.Void)]
        public void llScriptProfiler([LSLParam(LSLType.Integer)] LSL_Integer flags)
        {
            m_LSL_Functions.llScriptProfiler(flags);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key llSendRemoteData([LSLParam(LSLType.String)] string channel,
            [LSLParam(LSLType.String)] string dest, [LSLParam(LSLType.Integer)] int idata,
            [LSLParam(LSLType.String)] string sdata)
        {
            return m_LSL_Functions.llSendRemoteData(channel, dest, idata, sdata);
        }

        [LSLFunction(LSLType.Void)]
        public void llSensor([LSLParam(LSLType.String)] string name, [LSLParam(LSLType.String)] string id,
            [LSLParam(LSLType.Integer)] int type, [LSLParam(LSLType.Float)] double range,
            [LSLParam(LSLType.Float)] double arc)
        {
            m_LSL_Functions.llSensor(name, id, type, range, arc);
        }

        [LSLFunction(LSLType.Void)]
        public void llSensorRemove()
        {
            m_LSL_Functions.llSensorRemove();
        }

        [LSLFunction(LSLType.Void)]
        public void llSensorRepeat([LSLParam(LSLType.String)] string name, [LSLParam(LSLType.String)] string id,
            [LSLParam(LSLType.Integer)] int type, [LSLParam(LSLType.Float)] double range,
            [LSLParam(LSLType.Float)] double arc, [LSLParam(LSLType.Float)] double rate)
        {
            m_LSL_Functions.llSensorRepeat(name, id, type, range, arc, rate);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetAlpha([LSLParam(LSLType.Float)] double alpha, [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llSetAlpha(alpha, face);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetBuoyancy([LSLParam(LSLType.Float)] double buoyancy)
        {
            m_LSL_Functions.llSetBuoyancy(buoyancy);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetCameraAtOffset([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            m_LSL_Functions.llSetCameraAtOffset(offset);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetCameraEyeOffset([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            m_LSL_Functions.llSetCameraEyeOffset(offset);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLinkCamera([LSLParam(LSLType.Integer)] LSL_Integer link,
            [LSLParam(LSLType.Vector)] LSL_Vector eye, [LSLParam(LSLType.Vector)] LSL_Vector at)
        {
            m_LSL_Functions.llSetLinkCamera(link, eye, at);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetCameraParams([LSLParam(LSLType.List)] LSL_List rules)
        {
            m_LSL_Functions.llSetCameraParams(rules);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetClickAction([LSLParam(LSLType.Integer)] int action)
        {
            m_LSL_Functions.llSetClickAction(action);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetColor([LSLParam(LSLType.Vector)] LSL_Vector color, [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llSetColor(color, face);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetContentType([LSLParam(LSLType.Key)] LSL_Key id, [LSLParam(LSLType.Integer)] LSL_Integer type)
        {
            m_LSL_Functions.llSetContentType(id, type);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetDamage([LSLParam(LSLType.Float)] double damage)
        {
            m_LSL_Functions.llSetDamage(damage);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetForce([LSLParam(LSLType.Vector)] LSL_Vector force, [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llSetForce(force, local);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetForceAndTorque([LSLParam(LSLType.Vector)] LSL_Vector force,
            [LSLParam(LSLType.Vector)] LSL_Vector torque, [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llSetForceAndTorque(force, torque, local);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetVelocity([LSLParam(LSLType.Vector)] LSL_Vector force, [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llSetVelocity(force, local);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetAngularVelocity([LSLParam(LSLType.Vector)] LSL_Vector force,
            [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llSetAngularVelocity(force, local);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetHoverHeight([LSLParam(LSLType.Float)] double height, [LSLParam(LSLType.Integer)] int water,
            [LSLParam(LSLType.Float)] double tau)
        {
            m_LSL_Functions.llSetHoverHeight(height, water, tau);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetInventoryPermMask([LSLParam(LSLType.String)] string item, [LSLParam(LSLType.Integer)] int mask,
            [LSLParam(LSLType.Integer)] int value)
        {
            m_LSL_Functions.llSetInventoryPermMask(item, mask, value);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLinkAlpha([LSLParam(LSLType.Integer)] int linknumber, [LSLParam(LSLType.Float)] double alpha,
            [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llSetLinkAlpha(linknumber, alpha, face);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLinkColor([LSLParam(LSLType.Integer)] int linknumber,
            [LSLParam(LSLType.Vector)] LSL_Vector color, [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llSetLinkColor(linknumber, color, face);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLinkPrimitiveParams([LSLParam(LSLType.Integer)] int linknumber,
            [LSLParam(LSLType.List)] LSL_List rules)
        {
            m_LSL_Functions.llSetLinkPrimitiveParams(linknumber, rules);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLinkTexture([LSLParam(LSLType.Integer)] int linknumber,
            [LSLParam(LSLType.String)] string texture, [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llSetLinkTexture(linknumber, texture, face);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLinkTextureAnim([LSLParam(LSLType.Integer)] int linknum, [LSLParam(LSLType.Integer)] int mode,
            [LSLParam(LSLType.Integer)] int face, [LSLParam(LSLType.Integer)] int sizex,
            [LSLParam(LSLType.Integer)] int sizey, [LSLParam(LSLType.Float)] double start,
            double length, [LSLParam(LSLType.Float)] double rate)
        {
            m_LSL_Functions.llSetLinkTextureAnim(linknum, mode, face, sizex, sizey, start, length, rate);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLocalRot([LSLParam(LSLType.Rotation)] LSL_Rotation rot)
        {
            m_LSL_Functions.llSetLocalRot(rot);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llSetMemoryLimit([LSLParam(LSLType.Integer)] LSL_Integer limit)
        {
            return m_LSL_Functions.llSetMemoryLimit(limit);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetObjectDesc([LSLParam(LSLType.String)] string desc)
        {
            m_LSL_Functions.llSetObjectDesc(desc);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetObjectName([LSLParam(LSLType.String)] string name)
        {
            m_LSL_Functions.llSetObjectName(name);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetObjectPermMask([LSLParam(LSLType.Integer)] int mask, [LSLParam(LSLType.Integer)] int value)
        {
            m_LSL_Functions.llSetObjectPermMask(mask, value);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetParcelMusicURL([LSLParam(LSLType.String)] string url)
        {
            m_LSL_Functions.llSetParcelMusicURL(url);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetPayPrice([LSLParam(LSLType.Integer)] int price,
            [LSLParam(LSLType.List)] LSL_List quick_pay_buttons)
        {
            m_LSL_Functions.llSetPayPrice(price, quick_pay_buttons);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetPos([LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            m_LSL_Functions.llSetPos(pos);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetPrimitiveParams([LSLParam(LSLType.List)] LSL_List rules)
        {
            m_LSL_Functions.llSetPrimitiveParams(rules);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetLinkPrimitiveParamsFast([LSLParam(LSLType.Integer)] int linknum,
            [LSLParam(LSLType.List)] LSL_List rules)
        {
            m_LSL_Functions.llSetLinkPrimitiveParamsFast(linknum, rules);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llSetPrimURL([LSLParam(LSLType.String)] string url)
        {
            m_LSL_Functions.llSetPrimURL(url);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llSetRegionPos([LSLParam(LSLType.Vector)] LSL_Vector pos)
        {
            return m_LSL_Functions.llSetRegionPos(pos);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetRemoteScriptAccessPin([LSLParam(LSLType.Integer)] int pin)
        {
            m_LSL_Functions.llSetRemoteScriptAccessPin(pin);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetRot([LSLParam(LSLType.Rotation)] LSL_Rotation rot)
        {
            m_LSL_Functions.llSetRot(rot);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetScale([LSLParam(LSLType.Vector)] LSL_Vector scale)
        {
            m_LSL_Functions.llSetScale(scale);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetScriptState([LSLParam(LSLType.String)] string name, [LSLParam(LSLType.Integer)] int run)
        {
            m_LSL_Functions.llSetScriptState(name, run);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetSitText([LSLParam(LSLType.String)] string text)
        {
            m_LSL_Functions.llSetSitText(text);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetSoundQueueing([LSLParam(LSLType.Integer)] int queue)
        {
            m_LSL_Functions.llSetSoundQueueing(queue);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetSoundRadius([LSLParam(LSLType.Float)] double radius)
        {
            m_LSL_Functions.llSetSoundRadius(radius);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetStatus([LSLParam(LSLType.Integer)] int status, [LSLParam(LSLType.Integer)] int value)
        {
            m_LSL_Functions.llSetStatus(status, value);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetText([LSLParam(LSLType.String)] string text, [LSLParam(LSLType.Vector)] LSL_Vector color,
            [LSLParam(LSLType.Float)] double alpha)
        {
            m_LSL_Functions.llSetText(text, color, alpha);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetTexture([LSLParam(LSLType.String)] string texture, [LSLParam(LSLType.Integer)] int face)
        {
            m_LSL_Functions.llSetTexture(texture, face);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetTextureAnim([LSLParam(LSLType.Integer)] int mode, [LSLParam(LSLType.Integer)] int face,
            [LSLParam(LSLType.Integer)] int sizex, [LSLParam(LSLType.Integer)] int sizey,
            [LSLParam(LSLType.Float)] double start, [LSLParam(LSLType.Float)] double length,
            [LSLParam(LSLType.Float)] double rate)
        {
            m_LSL_Functions.llSetTextureAnim(mode, face, sizex, sizey, start, length, rate);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetTimerEvent([LSLParam(LSLType.Float)] double sec)
        {
            m_LSL_Functions.llSetTimerEvent(sec);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetTorque([LSLParam(LSLType.Vector)] LSL_Vector torque, [LSLParam(LSLType.Integer)] int local)
        {
            m_LSL_Functions.llSetTorque(torque, local);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetTouchText([LSLParam(LSLType.String)] string text)
        {
            m_LSL_Functions.llSetTouchText(text);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetVehicleFlags([LSLParam(LSLType.Integer)] int flags)
        {
            m_LSL_Functions.llSetVehicleFlags(flags);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetVehicleFloatParam([LSLParam(LSLType.Integer)] int param,
            [LSLParam(LSLType.Float)] LSL_Float value)
        {
            m_LSL_Functions.llSetVehicleFloatParam(param, value);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetVehicleRotationParam([LSLParam(LSLType.Integer)] int param,
            [LSLParam(LSLType.Rotation)] LSL_Rotation rot)
        {
            m_LSL_Functions.llSetVehicleRotationParam(param, rot);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetVehicleType([LSLParam(LSLType.Integer)] int type)
        {
            m_LSL_Functions.llSetVehicleType(type);
        }

        [LSLFunction(LSLType.Void)]
        public void llSetVehicleVectorParam([LSLParam(LSLType.Integer)] int param,
            [LSLParam(LSLType.Vector)] LSL_Vector vec)
        {
            m_LSL_Functions.llSetVehicleVectorParam(param, vec);
        }

        [LSLFunction(LSLType.Void)]
        public void llShout([LSLParam(LSLType.Integer)] int channelID, [LSLParam(LSLType.String)] string text)
        {
            m_LSL_Functions.llShout(channelID, text);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llSin([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llSin(f);
        }

        [LSLFunction(LSLType.Void)]
        public void llSitTarget([LSLParam(LSLType.Vector)] LSL_Vector offset,
            [LSLParam(LSLType.Rotation)] LSL_Rotation rot)
        {
            m_LSL_Functions.llSitTarget(offset, rot);
        }

        [LSLFunction(LSLType.Void)]
        public void llLinkSitTarget([LSLParam(LSLType.Integer)] LSL_Integer link,
            [LSLParam(LSLType.Vector)] LSL_Vector offset, [LSLParam(LSLType.Rotation)] LSL_Rotation rot)
        {
            m_LSL_Functions.llLinkSitTarget(link, offset, rot);
        }

        [LSLFunction(LSLType.Void)]
        public void llSleep([LSLParam(LSLType.Float)] double sec)
        {
            m_LSL_Functions.llSleep(sec);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llSound([LSLParam(LSLType.String)] string sound, [LSLParam(LSLType.Float)] double volume,
            [LSLParam(LSLType.Integer)] int queue, [LSLParam(LSLType.Integer)] int loop)
        {
            m_LSL_Functions.llSound(sound, volume, queue, loop);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llSoundPreload([LSLParam(LSLType.String)] string sound)
        {
            m_LSL_Functions.llSoundPreload(sound);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llSqrt([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llSqrt(f);
        }

        [LSLFunction(LSLType.Void)]
        public void llStartAnimation([LSLParam(LSLType.String)] string anim)
        {
            m_LSL_Functions.llStartAnimation(anim);
        }

        [LSLFunction(LSLType.Void)]
        public void llStopAnimation([LSLParam(LSLType.String)] string anim)
        {
            m_LSL_Functions.llStopAnimation(anim);
        }

        [LSLFunction(LSLType.Void)]
        public void llStopHover()
        {
            m_LSL_Functions.llStopHover();
        }

        [LSLFunction(LSLType.Void)]
        public void llStopLookAt()
        {
            m_LSL_Functions.llStopLookAt();
        }

        [LSLFunction(LSLType.Void)]
        public void llStopMoveToTarget()
        {
            m_LSL_Functions.llStopMoveToTarget();
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llStopPointAt()
        {
            m_LSL_Functions.llStopPointAt();
        }

        [LSLFunction(LSLType.Void)]
        public void llStopSound()
        {
            m_LSL_Functions.llStopSound();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llStringLength([LSLParam(LSLType.String)] string str)
        {
            return m_LSL_Functions.llStringLength(str);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llStringToBase64([LSLParam(LSLType.String)] string str)
        {
            return m_LSL_Functions.llStringToBase64(str);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llStringTrim([LSLParam(LSLType.String)] string src, [LSLParam(LSLType.Integer)] int type)
        {
            return m_LSL_Functions.llStringTrim(src, type);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llSubStringIndex([LSLParam(LSLType.String)] string source,
            [LSLParam(LSLType.String)] string pattern)
        {
            return m_LSL_Functions.llSubStringIndex(source, pattern);
        }

        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void llTakeCamera([LSLParam(LSLType.String)] string avatar)
        {
            m_LSL_Functions.llTakeCamera(avatar);
        }

        [LSLFunction(LSLType.Void)]
        public void llTakeControls([LSLParam(LSLType.Integer)] int controls, [LSLParam(LSLType.Integer)] int accept,
            [LSLParam(LSLType.Integer)] int pass_on)
        {
            m_LSL_Functions.llTakeControls(controls, accept, pass_on);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llTan([LSLParam(LSLType.Float)] double f)
        {
            return m_LSL_Functions.llTan(f);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llTarget([LSLParam(LSLType.Vector)] LSL_Vector position,
            [LSLParam(LSLType.Float)] double range)
        {
            return m_LSL_Functions.llTarget(position, range);
        }

        [LSLFunction(LSLType.Void)]
        public void llTargetOmega([LSLParam(LSLType.Vector)] LSL_Vector axis, [LSLParam(LSLType.Float)] double spinrate,
            [LSLParam(LSLType.Float)] double gain)
        {
            m_LSL_Functions.llTargetOmega(axis, spinrate, gain);
        }

        [LSLFunction(LSLType.Void)]
        public void llTargetRemove([LSLParam(LSLType.Integer)] int number)
        {
            m_LSL_Functions.llTargetRemove(number);
        }

        [LSLFunction(LSLType.Void)]
        public void llTeleportAgent([LSLParam(LSLType.String)] string agent, [LSLParam(LSLType.String)] string simname,
            [LSLParam(LSLType.Vector)] LSL_Vector pos, [LSLParam(LSLType.Vector)] LSL_Vector lookAt)
        {
            m_LSL_Functions.llTeleportAgent(agent, simname, pos, lookAt);
        }

        [LSLFunction(LSLType.Void)]
        public void llTeleportAgentGlobalCoords([LSLParam(LSLType.String)] string agent,
            [LSLParam(LSLType.Vector)] LSL_Vector global, [LSLParam(LSLType.Vector)] LSL_Vector pos,
            [LSLParam(LSLType.Vector)] LSL_Vector lookAt)
        {
            m_LSL_Functions.llTeleportAgentGlobalCoords(agent, global, pos, lookAt);
        }

        [LSLFunction(LSLType.Void)]
        public void llTeleportAgentHome([LSLParam(LSLType.String)] string agent)
        {
            m_LSL_Functions.llTeleportAgentHome(agent);
        }

        [LSLFunction(LSLType.Void)]
        public void llTextBox([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.String)] string message,
            [LSLParam(LSLType.Integer)] int chat_channel)
        {
            m_LSL_Functions.llTextBox(avatar, message, chat_channel);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llToLower([LSLParam(LSLType.String)] string source)
        {
            return m_LSL_Functions.llToLower(source);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llToUpper([LSLParam(LSLType.String)] string source)
        {
            return m_LSL_Functions.llToUpper(source);
        }

        [LSLFunction(LSLType.Void)]
        public void llTriggerSound([LSLParam(LSLType.String)] string sound, [LSLParam(LSLType.Float)] double volume)
        {
            m_LSL_Functions.llTriggerSound(sound, volume);
        }

        [LSLFunction(LSLType.Void)]
        public void llTriggerSoundLimited([LSLParam(LSLType.String)] string sound,
            [LSLParam(LSLType.Float)] double volume, [LSLParam(LSLType.Vector)] LSL_Vector top_north_east,
            LSL_Vector bottom_south_west)
        {
            m_LSL_Functions.llTriggerSoundLimited(sound, volume, top_north_east, bottom_south_west);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String llUnescapeURL([LSLParam(LSLType.String)] string url)
        {
            return m_LSL_Functions.llUnescapeURL(url);
        }

        [LSLFunction(LSLType.Void)]
        public void llUnSit([LSLParam(LSLType.String)] string id)
        {
            m_LSL_Functions.llUnSit(id);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llVecDist([LSLParam(LSLType.Vector)] LSL_Vector a, [LSLParam(LSLType.Vector)] LSL_Vector b)
        {
            return m_LSL_Functions.llVecDist(a, b);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llVecMag([LSLParam(LSLType.Vector)] LSL_Vector v)
        {
            return m_LSL_Functions.llVecMag(v);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llVecNorm([LSLParam(LSLType.Vector)] LSL_Vector v)
        {
            return m_LSL_Functions.llVecNorm(v);
        }

        [LSLFunction(LSLType.Void)]
        public void llVolumeDetect([LSLParam(LSLType.Integer)] int detect)
        {
            m_LSL_Functions.llVolumeDetect(detect);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float llWater([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            return m_LSL_Functions.llWater(offset);
        }

        [LSLFunction(LSLType.Void)]
        public void llWhisper([LSLParam(LSLType.Integer)] int channelID, [LSLParam(LSLType.String)] string text)
        {
            m_LSL_Functions.llWhisper(channelID, text);
        }

        [LSLFunction(LSLType.Vector)]
        public LSL_Vector llWind([LSLParam(LSLType.Vector)] LSL_Vector offset)
        {
            return m_LSL_Functions.llWind(offset);
        }

        [LSLFunction(LSLType.String, Deprecated = true)]
        public LSL_String llXorBase64Strings([LSLParam(LSLType.String)] string str1,
            [LSLParam(LSLType.String)] string str2)
        {
            return m_LSL_Functions.llXorBase64Strings(str1, str2);
        }

        [LSLFunction(LSLType.String, Deprecated = true)]
        public LSL_String llXorBase64StringsCorrect([LSLParam(LSLType.String)] string str1,
            [LSLParam(LSLType.String)] string str2)
        {
            return m_LSL_Functions.llXorBase64StringsCorrect(str1, str2);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetPrimMediaParams([LSLParam(LSLType.Integer)] int face,
            [LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_LSL_Functions.llGetPrimMediaParams(face, rules);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List llGetLinkMedia([LSLParam(LSLType.Integer)] LSL_Integer link,
            [LSLParam(LSLType.Integer)] LSL_Integer face, [LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_LSL_Functions.llGetLinkMedia(link, face, rules);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llSetPrimMediaParams([LSLParam(LSLType.Integer)] int face,
            [LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_LSL_Functions.llSetPrimMediaParams(face, rules);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llSetLinkMedia([LSLParam(LSLType.Integer)] LSL_Integer link,
            [LSLParam(LSLType.Integer)] LSL_Integer face, [LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_LSL_Functions.llSetLinkMedia(link, face, rules);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llClearPrimMedia([LSLParam(LSLType.Integer)] LSL_Integer face)
        {
            return m_LSL_Functions.llClearPrimMedia(face);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer llClearLinkMedia([LSLParam(LSLType.Integer)] LSL_Integer link,
            [LSLParam(LSLType.Integer)] LSL_Integer face)
        {
            return m_LSL_Functions.llClearLinkMedia(link, face);
        }

        [LSLFunction(LSLType.Void)]
        public void print([LSLParam(LSLType.String)] string str)
        {
            m_LSL_Functions.print(str);
        }
    }
}