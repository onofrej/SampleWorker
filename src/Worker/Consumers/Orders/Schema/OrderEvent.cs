// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.7.7.5
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace onofrej.github.io
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	public partial class OrderEvent : ISpecificRecord
	{
		public static Schema _SCHEMA = Schema.Parse(@"{""type"":""record"",""name"":""OrderEvent"",""namespace"":""onofrej.github.io"",""fields"":[{""name"":""OrderId"",""doc"":""Identification code of the order"",""type"":""string""},{""name"":""Value"",""doc"":""Value of the order"",""type"":""double""},{""name"":""OrderDate"",""doc"":""Date and time of the order (UTC)"",""type"":""string""},{""name"":""ClientId"",""doc"":""Identification code of the client"",""type"":""string""}],""doc:"":""A basic schema for storing orders""}");
		/// <summary>
		/// Identification code of the order
		/// </summary>
		private string _OrderId;
		/// <summary>
		/// Value of the order
		/// </summary>
		private double _Value;
		/// <summary>
		/// Date and time of the order (UTC)
		/// </summary>
		private string _OrderDate;
		/// <summary>
		/// Identification code of the client
		/// </summary>
		private string _ClientId;
		public virtual Schema Schema
		{
			get
			{
				return OrderEvent._SCHEMA;
			}
		}
		/// <summary>
		/// Identification code of the order
		/// </summary>
		public string OrderId
		{
			get
			{
				return this._OrderId;
			}
			set
			{
				this._OrderId = value;
			}
		}
		/// <summary>
		/// Value of the order
		/// </summary>
		public double Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
			}
		}
		/// <summary>
		/// Date and time of the order (UTC)
		/// </summary>
		public string OrderDate
		{
			get
			{
				return this._OrderDate;
			}
			set
			{
				this._OrderDate = value;
			}
		}
		/// <summary>
		/// Identification code of the client
		/// </summary>
		public string ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.OrderId;
			case 1: return this.Value;
			case 2: return this.OrderDate;
			case 3: return this.ClientId;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.OrderId = (System.String)fieldValue; break;
			case 1: this.Value = (System.Double)fieldValue; break;
			case 2: this.OrderDate = (System.String)fieldValue; break;
			case 3: this.ClientId = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}