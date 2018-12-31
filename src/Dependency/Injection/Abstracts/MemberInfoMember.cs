﻿using System;
using System.Reflection;

namespace Unity.Injection
{
    public abstract class MemberInfoMember<TMemberInfo> : InjectionMember<TMemberInfo, object>
                                      where TMemberInfo : MemberInfo
    {
        #region Constructors

        protected MemberInfoMember(string name, object data) 
            : base(name, data)
        {
        }

        protected MemberInfoMember(TMemberInfo info, object data)
            : base(info, data)
        {
        }


        #endregion


        #region Overrides

        public override (TMemberInfo, object) FromType(Type type)
        {
            return ReferenceEquals(Data, ResolvedValue) 
                ? (MemberInfo, MemberInfo) 
                : (MemberInfo, Data);
        }

#if NETSTANDARD1_0
        public override bool Equals(TMemberInfo other)
        {
            return null != other && other.Name == Name;
        }
#endif
        #endregion
    }
}