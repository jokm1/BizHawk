﻿using BizHawk.Emulation.Common;

namespace BizHawk.Emulation.Cores.Nintendo.GBHawkLink
{
	public partial class GBHawkLink : IInputPollable
	{
		public int LagCount
		{
			get { return _lagcount; }
			set { _lagcount = value; }
		}

		public bool IsLagFrame
		{
			get { return _islag; }
			set { _islag = value; }
		}

		public IInputCallbackSystem InputCallbacks { get; } = new InputCallbackSystem();

		public bool _islag = true;
		private int _lagcount;
	}
}
