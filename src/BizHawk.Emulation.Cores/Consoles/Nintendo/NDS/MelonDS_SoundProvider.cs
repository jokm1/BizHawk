﻿using System;
using System.Runtime.InteropServices;

using BizHawk.Emulation.Common;

namespace BizHawk.Emulation.Cores.Consoles.Nintendo.NDS
{
	unsafe partial class MelonDS : ISoundProvider
	{
		private readonly SpeexResampler _resampler;

		public bool CanProvideAsync => false;

		public SyncSoundMode SyncMode => SyncSoundMode.Sync;

		public void DiscardSamples()
		{
			_DiscardSamples();
		}

		public void GetSamplesAsync(short[] samples)
		{
			throw new InvalidOperationException();
		}

		public void GetSamplesSync(out short[] samples, out int nsamp)
		{
			nsamp = GetSampleCount();
			samples = new short[nsamp * 2]; //*2 for stereo sound
			fixed (short* data = samples)
			{
				GetSamples(data, nsamp);
			}
			// BizHawk requires a sample rate of 44100 Hz.
			_resampler.EnqueueSamples(samples, nsamp);
			_resampler.GetSamplesSync(out samples, out nsamp);
		}

		public void SetSyncMode(SyncSoundMode mode)
		{
			if (mode == SyncSoundMode.Async)
				throw new InvalidOperationException();
		}

		[DllImport(dllPath)]
		private static extern int GetSampleCount();
		[DllImport(dllPath)]
		private static extern void GetSamples(short* data, int count);
		[DllImport(dllPath, EntryPoint = "DiscardSamples")]
		private static extern void _DiscardSamples();
	}
}
