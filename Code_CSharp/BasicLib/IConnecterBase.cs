using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// IConnecterBase ��ժҪ˵����
	/// </summary>
	public interface IConnecterBase
	{
		bool RefreshConnection();

		bool IsAvailable(); // being on connection, or resource is availabe

		bool CloseConnection();

	}
}
