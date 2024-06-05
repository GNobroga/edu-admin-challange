namespace EduAdmin.Common.Base;

public abstract class BaseEntity<ID> 
{
    public ID? Id { get; set; }
}