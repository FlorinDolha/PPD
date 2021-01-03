using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

public class UnitOfWork : IDisposable
{
    private Context context = new Context();
    private Repository<Spectacol> spectacolRepository;
    private Repository<Vanzare> vanzareRepository;
    private Repository<VanzariLocuri> vanzariLocuriRepository;
    private Repository<Verificare> verificareRepository;
    private Repository<Sala> salaRepository;

    public Repository<Spectacol> SpectacolRepository
    {
        get
        {
            if (spectacolRepository == null)
            {
                spectacolRepository = new Repository<Spectacol>(context);
            }

            return spectacolRepository;
        }
    }

    public Repository<Vanzare> VanzareRepository
    {
        get
        {
            if (vanzareRepository == null)
            {
                vanzareRepository = new Repository<Vanzare>(context);
            }

            return vanzareRepository;
        }
    }

    public Repository<VanzariLocuri> VanzariLocuriRepository
    {
        get
        {
            if (vanzariLocuriRepository == null)
            {
                vanzariLocuriRepository = new Repository<VanzariLocuri>(context);
            }

            return vanzariLocuriRepository;
        }
    }

    public Repository<Verificare> VerificareRepository
    {
        get
        {
            if (verificareRepository == null)
            {
                verificareRepository = new Repository<Verificare>(context);
            }

            return verificareRepository;
        }
    }

    public Repository<Sala> SalaRepository
    {
        get
        {
            if (salaRepository == null)
            {
                salaRepository = new Repository<Sala>(context);
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

    public IList<int> LocuriLibere(int spectacolId)
    {
        IList<int> locuriSala = Enumerable.Range(1, SalaRepository.GetByID(1).NrLocuri).ToList();

        IList<int> locuriVandute = VanzariLocuriRepository.Get()
                                                          .Where(vanzareLoc => VanzareaEstePentruSpectacol(vanzareLoc.VanzareId, spectacolId))
                                                          .Select(vanzareLoc => vanzareLoc.Loc).ToList();

        return locuriSala.Where(loc => !locuriVandute.Contains(loc)).ToList();
    }
}