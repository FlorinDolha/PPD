using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

public class UnitOfWork : IDisposable
{
    private Context context = new Context();
    private GenericRepository<Spectacol> spectacolRepository;
    private GenericRepository<Vanzare> vanzareRepository;
    private GenericRepository<VanzariLocuri> vanzariLocuriRepository;
    private GenericRepository<Verificare> verificareRepository;
    private GenericRepository<Sala> salaRepository;

    public GenericRepository<Spectacol> SpectacolRepository
    {
        get
        {
            if (spectacolRepository == null)
            {
                spectacolRepository = new GenericRepository<Spectacol>(context);
            }

            return spectacolRepository;
        }
    }

    public GenericRepository<Vanzare> VanzareRepository
    {
        get
        {
            if (vanzareRepository == null)
            {
                vanzareRepository = new GenericRepository<Vanzare>(context);
            }

            return vanzareRepository;
        }
    }

    public GenericRepository<VanzariLocuri> VanzariLocuriRepository
    {
        get
        {
            if (vanzariLocuriRepository == null)
            {
                vanzariLocuriRepository = new GenericRepository<VanzariLocuri>(context);
            }

            return vanzariLocuriRepository;
        }
    }

    public GenericRepository<Verificare> VerificareRepository
    {
        get
        {
            if (verificareRepository == null)
            {
                verificareRepository = new GenericRepository<Verificare>(context);
            }

            return verificareRepository;
        }
    }

    public GenericRepository<Sala> SalaRepository
    {
        get
        {
            if (salaRepository == null)
            {
                salaRepository = new GenericRepository<Sala>(context);
            }

            return salaRepository;
        }
    }

    public void Save()
    {
        context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool VanzareaEstePentruSpectacol(int vanzareId, int spectacolId)
    {
        Vanzare vanzare = VanzareRepository.GetByID(vanzareId);

        if (vanzare == null)
        {
            return false;
        }

        return vanzare.SpectacolId == spectacolId;
    }

    /// <summary>
    /// Cauta primul loc liber pentru spectacolId
    /// </summary>
    /// <param name="spectacolId"></param>
    /// <returns>Returneaza un intreg reprezentand locul liber, sau null daca nu exista</returns>
    public int? PrimulLocLiber(int spectacolId)
    {
        IList<int> locuriSala = Enumerable.Range(1, SalaRepository.GetByID(1).NrLocuri).ToList();

        IList<int> locuriVandute = VanzariLocuriRepository.Get()
                                                          .Where(vanzareLoc => VanzareaEstePentruSpectacol(vanzareLoc.VanzareId, spectacolId))
                                                          .Select(vanzareLoc => vanzareLoc.Loc).ToList();

        try
        {
            return locuriSala.First(loc => !locuriVandute.Contains(loc));
        }
        catch (InvalidOperationException ex)
        {
            return null;
        }
    }
}