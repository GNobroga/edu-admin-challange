
namespace EduAdmin.Common.Model;

public class PageRequest 
{

    private const int MAX_SIZE = Int32.MaxValue;

    private int _page = 0;

    public int Page { 
        get => _page; 
        set {
            if (value < 0) return;
            _page = value;
        } 
    }

    private int _size = MAX_SIZE;
  
    public int Size 
    { 
        get => _size; 
        set {
            if (value < 1) return;
            _size  = value > MAX_SIZE ? MAX_SIZE : value;
        }
    
    }
}