using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// IConnecterBase 的摘要说明。
	/// </summary>
	public interface IConnecterBase
	{
		bool RefreshConnection();

		bool IsAvailable(); // being on connection, or resource is availabe

		bool CloseConnection();

	}
}
