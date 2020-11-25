﻿//
// Copyright (c) Oxid Resolver. All rights reserved.
// 
//
// Licensed under the Non-Profit OSL. See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Runtime.Serialization
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, Inherited = false, AllowMultiple = true)]
	internal sealed class ContractNamespaceAttribute : Attribute
	{
		private string clrNamespace;

		private string contractNamespace;

		public string ClrNamespace
		{
			get
			{
				return this.clrNamespace;
			}
			set
			{
				this.clrNamespace = value;
			}
		}

		public string ContractNamespace
		{
			get
			{
				return this.contractNamespace;
			}
		}

		public ContractNamespaceAttribute(string contractNamespace)
		{
			this.contractNamespace = contractNamespace;
		}
	}

	// available in dotnet 3 but not on dotnet 2 which is needed for Windows 2000
	[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, AllowMultiple=false, Inherited=false)]
	internal sealed class IgnoreDataMemberAttribute : Attribute
	{
		public IgnoreDataMemberAttribute()
		{
		}
	}
}
