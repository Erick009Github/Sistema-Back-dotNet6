﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace SistemaErick2.Models;

public partial class BdsistemaContext : DbContext
{
    public BdsistemaContext()
    {
        
    }

    public BdsistemaContext(DbContextOptions<BdsistemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<DetalleIngreso> DetalleIngresos { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Idarticulo).HasName("PK__articulo__BCE2F8F70A5872C2");

            entity.ToTable("articulo");

            entity.HasIndex(e => e.Nombre, "UQ__articulo__72AFBCC6D08B2817").IsUnique();

            entity.Property(e => e.Idarticulo).HasColumnName("idarticulo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Condicion)
                .HasDefaultValueSql("((1))")
                .HasColumnName("condicion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioVenta).HasColumnName("precio_venta");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.CantidadVendida)
                .HasColumnName("cantidad_vendida")
                .HasDefaultValue(0);

            entity.Property(e => e.CantidadComprada)
                .HasColumnName("cantidad_comprada")
                .HasDefaultValue(0);

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__articulo__idcate__2A4B4B5E");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PK__categori__140587C7B6E7D5B0");

            entity.ToTable("categoria");

            entity.HasIndex(e => e.Nombre, "UQ__categori__72AFBCC687F570D7").IsUnique();

            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Condicion)
                .HasDefaultValueSql("((1))")
                .HasColumnName("condicion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetalleIngreso>(entity =>
        {
            entity.HasKey(e => e.IddetalleIngreso).HasName("PK__detalle___55124CDB32F184CD");

            entity.ToTable("detalle_ingreso");

            entity.Property(e => e.IddetalleIngreso).HasColumnName("iddetalle_ingreso");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Idarticulo).HasColumnName("idarticulo");
            entity.Property(e => e.Idingreso).HasColumnName("idingreso");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdarticuloNavigation).WithMany(p => p.DetalleIngresos)
                .HasForeignKey(d => d.Idarticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalle_i__idart__3A81B327");

            entity.HasOne(d => d.IdingresoNavigation).WithMany(p => p.DetalleIngresos)
                .HasForeignKey(d => d.Idingreso)
                .HasConstraintName("FK__detalle_i__iding__398D8EEE");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IddetalleVenta).HasName("PK__detalle___5CEC1648955CCF69");

            entity.ToTable("detalle_venta");

            entity.Property(e => e.IddetalleVenta).HasColumnName("iddetalle_venta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descuento).HasColumnName("descuento");
            entity.Property(e => e.Idarticulo).HasColumnName("idarticulo");
            entity.Property(e => e.Idventa).HasColumnName("idventa");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdarticuloNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.Idarticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalle_v__idart__4222D4EF");

            entity.HasOne(d => d.IdventaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.Idventa)
                .HasConstraintName("FK__detalle_v__idven__412EB0B6");
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.Idingreso).HasName("PK__ingreso__60BD709A46D13CC6");

            entity.ToTable("ingreso");

            entity.Property(e => e.Idingreso).HasColumnName("idingreso");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Impuesto).HasColumnName("impuesto");
            entity.Property(e => e.NumComprobante)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("num_comprobante");
            entity.Property(e => e.SerieComprobante)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("serie_comprobante");
            entity.Property(e => e.TipoComprobante)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_comprobante");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdproveedorNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.Idproveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingreso__idprove__35BCFE0A");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingreso__idusuar__36B12243");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Idpersona).HasName("PK__persona__5C5C1E28ABBE2727");

            entity.ToTable("persona");

            entity.Property(e => e.Idpersona).HasColumnName("idpersona");
            entity.Property(e => e.Direccion)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("num_documento");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");
            entity.Property(e => e.TipoPersona)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_persona");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("PK__rol__24C6BB204F83A5FB");

            entity.ToTable("rol");

            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Condicion)
                .HasDefaultValueSql("((1))")
                .HasColumnName("condicion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__usuario__080A9743E3A6AD0A");

            entity.ToTable("usuario");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Condicion)
                .HasDefaultValueSql("((1))")
                .HasColumnName("condicion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("num_documento");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(1)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(1)
                .HasColumnName("password_salt");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idrol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__idrol__32E0915F");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Idventa).HasName("PK__venta__F82D1AFB524131D3");

            entity.ToTable("venta");

            entity.Property(e => e.Idventa).HasColumnName("idventa");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Impuesto).HasColumnName("impuesto");
            entity.Property(e => e.NumComprobante)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("num_comprobante");
            entity.Property(e => e.SerieComprobante)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("serie_comprobante");
            entity.Property(e => e.TipoComprobante)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_comprobante");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Idcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__venta__idcliente__3D5E1FD2");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__venta__idusuario__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
