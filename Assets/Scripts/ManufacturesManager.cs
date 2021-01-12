using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GeneticAlgorithm;
using Random = UnityEngine.Random;

public class ManufacturesManager : MonoBehaviour
{
    public WorldDateTime worldDateTime;

    private const int ReproductionGensLiveDays = Settings.Basic.ManufactureLiveDays;
    private const int TopManufacturePart = 30; // 30%

    private List<Manufacture> _manufactures = new List<Manufacture>();
    private ProbabilityManager _probabilityManager = new ProbabilityManager();

    public Queue<ReproductionDnk> ReproductionGens = new Queue<ReproductionDnk>();

    private int _id = 1;

    private void Start()
    {
        worldDateTime.NewDay += WorldDateTimeNewDayHandler;
    }

    private void Awake()
    {
        var world = gameObject.transform.parent.gameObject;
        foreach (var manufacture in world.GetComponentsInChildren<Manufacture>())
        {
            _manufactures.Add(manufacture);
            manufacture.Id = _id;

            _id++;
        }
    }

    public List<Manufacture> GetManufactures()
    {
        return _manufactures;
    }

    public int GetManufactureId()
    {
        return ++_id;
    }

    public Dnk GetDnk()
    {
        Dnk dnk;
        if (ReproductionGens.Count != 0)
        {
            dnk = ReproductionGens.Dequeue().Dnk;
        }
        else
        {
            dnk = GetTopManufactureDnk();
        }

        //mutation probability: 20%
        if (_probabilityManager.IsProbability(20))
        {
            dnk.Mutate();

            Debug.Log("Mutation");
        }

        return dnk;
    }

    public void AddReproductionDnk(ReproductionDnk reproductionDnk)
    {
        ReproductionGens.Enqueue(reproductionDnk);
    }

    private Dnk GetTopManufactureDnk()
    {
        _manufactures.Sort(delegate(Manufacture x, Manufacture y)
        {
            if (x.Money < y.Money)
            {
                return 1;
            }

            if (x.Money > y.Money)
            {
                return -1;
            }

            return 0;
        });

        var topManufactureMaxIndex = (int) (_manufactures.Count * TopManufacturePart / 100f);

        var index = Random.Range(0, topManufactureMaxIndex);
        var manufacture = _manufactures[index];

        return (Dnk) manufacture.Dnk.Clone();
    }

    private void WorldDateTimeNewDayHandler(object sender, Event.WorldDateTimeEventArgs e)
    {
        ReproductionGens = new Queue<ReproductionDnk>(
            ReproductionGens.Where(x => e.Day - ReproductionGensLiveDays <= x.CreateDay)
        );
    }
}