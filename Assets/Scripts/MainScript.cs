using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _cubePrefab;
    [SerializeField]
    private StickmanController _stickmanController;
    [SerializeField]
    private Button _resetButton;

    [Header("Cubes settings")]
    [SerializeField]
    private int _hSize = 40;
    [SerializeField]
    private int _vSize = 10;
    [SerializeField]
    private int _hStep = 5;
    [SerializeField]
    private int _vStep = 3;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        GenerateCubes();
        _stickmanController.Initialize();
        _resetButton.onClick.AddListener(_stickmanController.SetDefault);        
    }

    // TODO: try using ECS
    private void GenerateCubes()
    {
        // generate pile of cubes
        for (int y = 3; y <= _vSize; y = y + _vStep)
        {
            for (int x = -_hSize; x < _hSize; x = x + _hStep)
            {
                for (int z = -_hSize; z < _hSize; z = z + _hStep)
                {
                    if ((new Vector2(x, z)).SqrMagnitude() < 50)
                        continue;
                                        
                    Instantiate(_cubePrefab, new Vector3(x, y, z), Random.rotation);                    
                }
            }
        }
    }

}
