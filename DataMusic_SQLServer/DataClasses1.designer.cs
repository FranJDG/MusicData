﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataMusic_SQLServer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="EntornoPruebas")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definiciones de métodos de extensibilidad
    partial void OnCreated();
    partial void InsertAutor(Autor instance);
    partial void UpdateAutor(Autor instance);
    partial void DeleteAutor(Autor instance);
    partial void InsertAlbum(Album instance);
    partial void UpdateAlbum(Album instance);
    partial void DeleteAlbum(Album instance);
    partial void InsertCancion(Cancion instance);
    partial void UpdateCancion(Cancion instance);
    partial void DeleteCancion(Cancion instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::DataMusic_SQLServer.Properties.Settings.Default.ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Autor> Autor
		{
			get
			{
				return this.GetTable<Autor>();
			}
		}
		
		public System.Data.Linq.Table<Album> Album
		{
			get
			{
				return this.GetTable<Album>();
			}
		}
		
		public System.Data.Linq.Table<Cancion> Cancion
		{
			get
			{
				return this.GetTable<Cancion>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Autor")]
	public partial class Autor : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Nombre;
		
		private EntitySet<Album> _Album;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    #endregion
		
		public Autor()
		{
			this._Album = new EntitySet<Album>(new Action<Album>(this.attach_Album), new Action<Album>(this.detach_Album));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				if ((this._Nombre != value))
				{
					this.OnNombreChanging(value);
					this.SendPropertyChanging();
					this._Nombre = value;
					this.SendPropertyChanged("Nombre");
					this.OnNombreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Autor_Album", Storage="_Album", ThisKey="Id", OtherKey="AutorId")]
		public EntitySet<Album> Album
		{
			get
			{
				return this._Album;
			}
			set
			{
				this._Album.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Album(Album entity)
		{
			this.SendPropertyChanging();
			entity.Autor = this;
		}
		
		private void detach_Album(Album entity)
		{
			this.SendPropertyChanging();
			entity.Autor = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Album")]
	public partial class Album : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Titulo;
		
		private int _Año;
		
		private int _AutorId;
		
		private System.Data.Linq.Binary _Portada;
		
		private EntitySet<Cancion> _Cancion;
		
		private EntityRef<Autor> _Autor;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTituloChanging(string value);
    partial void OnTituloChanged();
    partial void OnAñoChanging(int value);
    partial void OnAñoChanged();
    partial void OnAutorIdChanging(int value);
    partial void OnAutorIdChanged();
    partial void OnPortadaChanging(System.Data.Linq.Binary value);
    partial void OnPortadaChanged();
    #endregion
		
		public Album()
		{
			this._Cancion = new EntitySet<Cancion>(new Action<Cancion>(this.attach_Cancion), new Action<Cancion>(this.detach_Cancion));
			this._Autor = default(EntityRef<Autor>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Titulo", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Titulo
		{
			get
			{
				return this._Titulo;
			}
			set
			{
				if ((this._Titulo != value))
				{
					this.OnTituloChanging(value);
					this.SendPropertyChanging();
					this._Titulo = value;
					this.SendPropertyChanged("Titulo");
					this.OnTituloChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Año", DbType="Int NOT NULL")]
		public int Año
		{
			get
			{
				return this._Año;
			}
			set
			{
				if ((this._Año != value))
				{
					this.OnAñoChanging(value);
					this.SendPropertyChanging();
					this._Año = value;
					this.SendPropertyChanged("Año");
					this.OnAñoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AutorId", DbType="Int NOT NULL")]
		public int AutorId
		{
			get
			{
				return this._AutorId;
			}
			set
			{
				if ((this._AutorId != value))
				{
					if (this._Autor.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAutorIdChanging(value);
					this.SendPropertyChanging();
					this._AutorId = value;
					this.SendPropertyChanged("AutorId");
					this.OnAutorIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Portada", DbType="Image", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Portada
		{
			get
			{
				return this._Portada;
			}
			set
			{
				if ((this._Portada != value))
				{
					this.OnPortadaChanging(value);
					this.SendPropertyChanging();
					this._Portada = value;
					this.SendPropertyChanged("Portada");
					this.OnPortadaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Album_Cancion", Storage="_Cancion", ThisKey="Id", OtherKey="AlbumId")]
		public EntitySet<Cancion> Cancion
		{
			get
			{
				return this._Cancion;
			}
			set
			{
				this._Cancion.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Autor_Album", Storage="_Autor", ThisKey="AutorId", OtherKey="Id", IsForeignKey=true)]
		public Autor Autor
		{
			get
			{
				return this._Autor.Entity;
			}
			set
			{
				Autor previousValue = this._Autor.Entity;
				if (((previousValue != value) 
							|| (this._Autor.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Autor.Entity = null;
						previousValue.Album.Remove(this);
					}
					this._Autor.Entity = value;
					if ((value != null))
					{
						value.Album.Add(this);
						this._AutorId = value.Id;
					}
					else
					{
						this._AutorId = default(int);
					}
					this.SendPropertyChanged("Autor");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Cancion(Cancion entity)
		{
			this.SendPropertyChanging();
			entity.Album = this;
		}
		
		private void detach_Cancion(Cancion entity)
		{
			this.SendPropertyChanging();
			entity.Album = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Cancion")]
	public partial class Cancion : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Titulo;
		
		private int _Numero;
		
		private int _AlbumId;
		
		private EntityRef<Album> _Album;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTituloChanging(string value);
    partial void OnTituloChanged();
    partial void OnNumeroChanging(int value);
    partial void OnNumeroChanged();
    partial void OnAlbumIdChanging(int value);
    partial void OnAlbumIdChanged();
    #endregion
		
		public Cancion()
		{
			this._Album = default(EntityRef<Album>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Titulo", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Titulo
		{
			get
			{
				return this._Titulo;
			}
			set
			{
				if ((this._Titulo != value))
				{
					this.OnTituloChanging(value);
					this.SendPropertyChanging();
					this._Titulo = value;
					this.SendPropertyChanged("Titulo");
					this.OnTituloChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Numero", DbType="Int NOT NULL")]
		public int Numero
		{
			get
			{
				return this._Numero;
			}
			set
			{
				if ((this._Numero != value))
				{
					this.OnNumeroChanging(value);
					this.SendPropertyChanging();
					this._Numero = value;
					this.SendPropertyChanged("Numero");
					this.OnNumeroChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AlbumId", DbType="Int NOT NULL")]
		public int AlbumId
		{
			get
			{
				return this._AlbumId;
			}
			set
			{
				if ((this._AlbumId != value))
				{
					if (this._Album.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAlbumIdChanging(value);
					this.SendPropertyChanging();
					this._AlbumId = value;
					this.SendPropertyChanged("AlbumId");
					this.OnAlbumIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Album_Cancion", Storage="_Album", ThisKey="AlbumId", OtherKey="Id", IsForeignKey=true)]
		public Album Album
		{
			get
			{
				return this._Album.Entity;
			}
			set
			{
				Album previousValue = this._Album.Entity;
				if (((previousValue != value) 
							|| (this._Album.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Album.Entity = null;
						previousValue.Cancion.Remove(this);
					}
					this._Album.Entity = value;
					if ((value != null))
					{
						value.Cancion.Add(this);
						this._AlbumId = value.Id;
					}
					else
					{
						this._AlbumId = default(int);
					}
					this.SendPropertyChanged("Album");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591