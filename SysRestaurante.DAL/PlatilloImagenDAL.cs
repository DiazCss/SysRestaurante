using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.PlatilloImagenDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    internal class PlatilloImagenDAL : IPlatilloImagenBL
    {
        private readonly SysRestauranteDbContext dbContext;
        private readonly string imageFolderPath = "wwwroot/images/platillos"; 

        public PlatilloImagenDAL(SysRestauranteDbContext context)
        {
            dbContext = context;
            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath); 
            }
        }

        #region Métodos CRUD

        public async Task<int> CreateAsync(PlatilloImagenMantDTO pPlatilloImagenDTO, string imagePath)
{
    var platilloImagen = new PlatilloImagen
    {
        IdPlatillo = pPlatilloImagenDTO.IdPlatillo,
        ImagenPlatillo = imagePath 
    };

    dbContext.platilloImagen.Add(platilloImagen);
    return await dbContext.SaveChangesAsync();
}


public async Task<int> ModificarAsync(PlatilloImagenMantDTO pPlatilloImagenDTO)
{
    var platilloImagen = await dbContext.platilloImagen.FirstOrDefaultAsync(pi => pi.Id == pPlatilloImagenDTO.Id);

    if (platilloImagen != null)
    {
        platilloImagen.IdPlatillo = pPlatilloImagenDTO.IdPlatillo; 

        if (!string.IsNullOrEmpty(pPlatilloImagenDTO.ImagenPlatillo) && pPlatilloImagenDTO.ImagenPlatillo.Contains("images/platillos"))
        {
            platilloImagen.ImagenPlatillo = pPlatilloImagenDTO.ImagenPlatillo;
        }
        else if (!string.IsNullOrEmpty(pPlatilloImagenDTO.ImagenPlatillo))
        {
            var imageName = $"{pPlatilloImagenDTO.IdPlatillo}_{Path.GetRandomFileName()}.jpg";
            var imagePath = Path.Combine(imageFolderPath, imageName);

            await File.WriteAllBytesAsync(imagePath, Convert.FromBase64String(pPlatilloImagenDTO.ImagenPlatillo));
            platilloImagen.ImagenPlatillo = Path.Combine("images/platillos", imageName).Replace("\\", "/");
        }

        return await dbContext.SaveChangesAsync();
    }

    return 0;
}



        public async Task<int> EliminarAsync(PlatilloImagenMantDTO pPlatilloImagenDTO)
        {
            var platilloImagen = await dbContext.platilloImagen.FirstOrDefaultAsync(pi => pi.Id == pPlatilloImagenDTO.Id);

            if (platilloImagen != null)
            {
               
                if (File.Exists(platilloImagen.ImagenPlatillo))
                {
                    File.Delete(platilloImagen.ImagenPlatillo);
                }

                dbContext.platilloImagen.Remove(platilloImagen);
                return await dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<PlatilloImagenMantDTO> ObtenerPorIdAsync(PlatilloImagenMantDTO pPlatilloImagenDTO)
        {
            var platilloImagen = await dbContext.platilloImagen
                .Include(pi => pi.Platillo)
                .FirstOrDefaultAsync(pi => pi.Id == pPlatilloImagenDTO.Id);

            if (platilloImagen != null)
            {
                return new PlatilloImagenMantDTO
                {
                    Id = platilloImagen.Id,
                    IdPlatillo = platilloImagen.IdPlatillo.Value,
                    ImagenPlatillo = platilloImagen.ImagenPlatillo,                 
                };
            }

            return new PlatilloImagenMantDTO();
        }

        public async Task<List<PlatilloImagenMantDTO>> ObtenerTodosAsync()
        {
            var platilloImagenes = await dbContext.platilloImagen.Include(pi => pi.Platillo).ToListAsync();
            return platilloImagenes.Select(pi => new PlatilloImagenMantDTO
            {
                Id = pi.Id,
                IdPlatillo = pi.IdPlatillo.Value,
                ImagenPlatillo = pi.ImagenPlatillo ,
                  NombrePlatillo = pi.Platillo.Nombre,
                   DescripcionPlatillo =pi.Platillo.Descripcion
            }).ToList();
        }

        public async Task<PaginacionOutputDTO<List<PlatilloImagenMantDTO>>> BuscarAsync(PlatilloImagenBuscarDTO pPlatilloImagenBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<PlatilloImagenMantDTO>>();
            result.Data = new List<PlatilloImagenMantDTO>();

            var query = dbContext.platilloImagen
                .Include(pi => pi.Platillo)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pPlatilloImagenBuscarDTO.NombrePlatillo_Like))
            {
                query = query.Where(pi => pi.Platillo.Nombre.Contains(pPlatilloImagenBuscarDTO.NombrePlatillo_Like));
            }

            var platilloImagenes = await query.ToListAsync();
            result.Data = platilloImagenes.Select(pi => new PlatilloImagenMantDTO
            {
                Id = pi.Id,
                IdPlatillo = pi.IdPlatillo.Value,
                ImagenPlatillo = pi.ImagenPlatillo,
                NombrePlatillo = pi.Platillo.Nombre,
                DescripcionPlatillo =pi.Platillo.Descripcion
                
            }).ToList();

            return result;
        }

        public Task<int> CreateAsync(PlatilloImagenMantDTO pPlatilloImagenDTO)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
