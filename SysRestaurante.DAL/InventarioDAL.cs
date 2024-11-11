using System;
using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.InventarioDTOs;
using SysRestaurante.BL.DTOs.ProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;

namespace SysRestaurante.DAL;

public class InventarioDAL : IInventarioBL
{
    readonly SysRestauranteDbContext dbContext;
    public InventarioDAL(SysRestauranteDbContext context) => dbContext = context;
    #region filtros de busqueda
    internal IQueryable<Inventario> QuerySelect(IQueryable<Inventario> pQuery, InventarioBuscarDTO pInventario)
    {
        if (!string.IsNullOrWhiteSpace(pInventario.IdProducto_Inventario_Like))
            pQuery = pQuery.Where(i => i.IdProducto.ToString().Contains(pInventario.IdProducto_Inventario_Like));
        pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
        if (pInventario.Take > 0)
            pQuery = pQuery.Skip(pInventario.Skip).Take(pInventario.Take).AsQueryable();
        return pQuery;
    }
    #endregion

    public async Task<int> CreateAsync(InventarioMantDTO pInventarioMantDTO)
    {
        bool inventarioExistente = await dbContext.inventario.AnyAsync(i => i.IdProducto == pInventarioMantDTO.IdProducto);
        if (inventarioExistente)
        {
            return 0;
        }
        Inventario inventario = new Inventario()
        {
            IdProducto = pInventarioMantDTO.IdProducto,
            CantidadDisponible = pInventarioMantDTO.CantidadDisponible,
            CantidadMinima = pInventarioMantDTO.CantidadMinima,
            CostoUnitario = pInventarioMantDTO.CostoUnitario,
            FechaUltimaCompra = pInventarioMantDTO.FechaUltimaCompra,
            FechaCaducidadLote = pInventarioMantDTO.FechaCaducidadLote,
        };
        dbContext.Add(inventario);
        return await dbContext.SaveChangesAsync();
    }
    public async Task<int> EliminarAsync(InventarioMantDTO pInventarioMantDTO)
    {
        var inventario = await dbContext.inventario.Where(p => p.Id == pInventarioMantDTO.Id)
                        .Include(p => p.productos)
                        .FirstOrDefaultAsync();
        if (inventario != null && inventario.Id != 0)
        {
            dbContext.inventario.Remove(inventario);
            return await dbContext.SaveChangesAsync();
        }
        else
            return 0;
    }
    public async Task<int> ModificarAsync(InventarioMantDTO pInventarioMantDTO)
    {
        var inventario = await dbContext.inventario.Where(i => i.Id == pInventarioMantDTO.Id)
                        .Include(p => p.productos)
                        .FirstOrDefaultAsync();
        if (inventario != null && inventario.Id != 0)
        {
            inventario.IdProducto = pInventarioMantDTO.IdProducto;
            inventario.CantidadDisponible = pInventarioMantDTO.CantidadDisponible;
            inventario.CantidadMinima = pInventarioMantDTO.CantidadMinima;
            inventario.CostoUnitario = pInventarioMantDTO.CostoUnitario;
            inventario.FechaUltimaCompra = pInventarioMantDTO.FechaUltimaCompra;
            inventario.FechaCaducidadLote = pInventarioMantDTO.FechaCaducidadLote;
            dbContext.Update(inventario);
            return await dbContext.SaveChangesAsync();
        }
        else
            return 0;
    }

    public async Task<InventarioMantDTO> ObtenerPorIdAsync(InventarioMantDTO pInventarioMantDTO)
    {
        var inventario = await dbContext.inventario.Where(i => i.Id == pInventarioMantDTO.Id)
                    .Include(p => p.productos)
                    .FirstOrDefaultAsync();
        if (inventario != null)
        {
            return new InventarioMantDTO
            {
                Id = inventario.Id,
                IdProducto = inventario.IdProducto,
                CantidadDisponible = inventario.CantidadDisponible,
                CantidadMinima = inventario.CantidadMinima,
                CostoUnitario = inventario.CostoUnitario,
                FechaUltimaCompra = inventario.FechaUltimaCompra,
                FechaCaducidadLote = inventario.FechaCaducidadLote,
                producto = new ProductoManDTOs {Id = inventario.productos.Id, Nombre = inventario.productos.Nombre}
            };
        }
        else
            return new InventarioMantDTO();
    }

    public async Task<List<InventarioMantDTO>> ObtenerTodosAsync()
    {
        var inventarios = await dbContext.inventario
        .Include(p => p.productos)
        .ToListAsync();
        if (inventarios != null && inventarios.Count > 0)
        {
            var List = new List<InventarioMantDTO>();
            inventarios.ForEach(i => List.Add(new InventarioMantDTO
            {
                Id = i.Id,
                IdProducto = i.IdProducto,
                CantidadDisponible = i.CantidadDisponible,
                CantidadMinima = i.CantidadMinima,
                CostoUnitario = i.CostoUnitario,
                FechaUltimaCompra = i.FechaCaducidadLote,
                FechaCaducidadLote = i.FechaCaducidadLote,
                producto = new ProductoManDTOs {Id = i.productos.Id, Nombre = i.productos.Nombre}

            }));
            return List;
        }
        else
            return new List<InventarioMantDTO>();
    }

    public async Task<PaginacionOutputDTO<List<InventarioMantDTO>>> BuscarAsync(InventarioBuscarDTO pInventarioBuscarDTO)
    {
        var result = new PaginacionOutputDTO<List<InventarioMantDTO>>();
        result.Data = new List<InventarioMantDTO>();
        var select = dbContext.inventario.AsQueryable();

        select = QuerySelect(select, pInventarioBuscarDTO);
        select = select.Include(p => p.productos);
        var inventarios = await select.ToListAsync();
        if (inventarios.Count > 0)
        {
            if (pInventarioBuscarDTO.IsCount)
            {
                pInventarioBuscarDTO.Take = 0;
                var selectCount = dbContext.inventario.AsQueryable();
                result.Count = await QuerySelect(selectCount, pInventarioBuscarDTO).CountAsync();
            }
            inventarios.ForEach(i => result.Data.Add(new InventarioMantDTO
            {
                Id = i.Id,
                IdProducto = i.IdProducto,
                CantidadDisponible = i.CantidadDisponible,
                CantidadMinima = i.CantidadMinima,
                CostoUnitario = i.CostoUnitario,
                FechaUltimaCompra = i.FechaCaducidadLote,
                FechaCaducidadLote = i.FechaCaducidadLote,
                producto = new ProductoManDTOs {Id = i.productos.Id, Nombre = i.productos.Nombre}

            }));
        }
        return result;
    }
}

