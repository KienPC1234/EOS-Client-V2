﻿using System;

namespace NAudio.Dsp
{
	/// <summary>
	/// Summary description for FastFourierTransform.
	/// </summary>
	// Token: 0x0200009C RID: 156
	public static class FastFourierTransform
	{
		/// <summary>
		/// This computes an in-place complex-to-complex FFT 
		/// x and y are the real and imaginary arrays of 2^m points.
		/// </summary>
		// Token: 0x0600035F RID: 863 RVA: 0x0000B778 File Offset: 0x00009978
		public static void FFT(bool forward, int m, Complex[] data)
		{
			int num = 1;
			for (int i = 0; i < m; i++)
			{
				num *= 2;
			}
			int num2 = num >> 1;
			int j = 0;
			for (int i = 0; i < num - 1; i++)
			{
				if (i < j)
				{
					float x = data[i].X;
					float y = data[i].Y;
					data[i].X = data[j].X;
					data[i].Y = data[j].Y;
					data[j].X = x;
					data[j].Y = y;
				}
				int k;
				for (k = num2; k <= j; k >>= 1)
				{
					j -= k;
				}
				j += k;
			}
			float num3 = -1f;
			float num4 = 0f;
			int num5 = 1;
			for (int l = 0; l < m; l++)
			{
				int num6 = num5;
				num5 <<= 1;
				float num7 = 1f;
				float num8 = 0f;
				for (j = 0; j < num6; j++)
				{
					for (int i = j; i < num; i += num5)
					{
						int num9 = i + num6;
						float num10 = num7 * data[num9].X - num8 * data[num9].Y;
						float num11 = num7 * data[num9].Y + num8 * data[num9].X;
						data[num9].X = data[i].X - num10;
						data[num9].Y = data[i].Y - num11;
						int num12 = i;
						data[num12].X = data[num12].X + num10;
						int num13 = i;
						data[num13].Y = data[num13].Y + num11;
					}
					float num14 = num7 * num3 - num8 * num4;
					num8 = num7 * num4 + num8 * num3;
					num7 = num14;
				}
				num4 = (float)Math.Sqrt((double)((1f - num3) / 2f));
				if (forward)
				{
					num4 = -num4;
				}
				num3 = (float)Math.Sqrt((double)((1f + num3) / 2f));
			}
			if (forward)
			{
				for (int i = 0; i < num; i++)
				{
					int num15 = i;
					data[num15].X = data[num15].X / (float)num;
					int num16 = i;
					data[num16].Y = data[num16].Y / (float)num;
				}
			}
		}

		/// <summary>
		/// Applies a Hamming Window
		/// </summary>
		/// <param name="n">Index into frame</param>
		/// <param name="frameSize">Frame size (e.g. 1024)</param>
		/// <returns>Multiplier for Hamming window</returns>
		// Token: 0x06000360 RID: 864 RVA: 0x0000B9D2 File Offset: 0x00009BD2
		public static double HammingWindow(int n, int frameSize)
		{
			return 0.54 - 0.46 * Math.Cos(6.2831853071795862 * (double)n / (double)(frameSize - 1));
		}

		/// <summary>
		/// Applies a Hann Window
		/// </summary>
		/// <param name="n">Index into frame</param>
		/// <param name="frameSize">Frame size (e.g. 1024)</param>
		/// <returns>Multiplier for Hann window</returns>
		// Token: 0x06000361 RID: 865 RVA: 0x0000B9FE File Offset: 0x00009BFE
		public static double HannWindow(int n, int frameSize)
		{
			return 0.5 * (1.0 - Math.Cos(6.2831853071795862 * (double)n / (double)(frameSize - 1)));
		}

		/// <summary>
		/// Applies a Blackman-Harris Window
		/// </summary>
		/// <param name="n">Index into frame</param>
		/// <param name="frameSize">Frame size (e.g. 1024)</param>
		/// <returns>Multiplier for Blackmann-Harris window</returns>
		// Token: 0x06000362 RID: 866 RVA: 0x0000BA2C File Offset: 0x00009C2C
		public static double BlackmannHarrisWindow(int n, int frameSize)
		{
			return 0.35875 - 0.48829 * Math.Cos(6.2831853071795862 * (double)n / (double)(frameSize - 1)) + 0.14128 * Math.Cos(12.566370614359172 * (double)n / (double)(frameSize - 1)) - 0.01168 * Math.Cos(18.849555921538759 * (double)n / (double)(frameSize - 1));
		}
	}
}
