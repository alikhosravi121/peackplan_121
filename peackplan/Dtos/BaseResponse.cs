namespace peackplan.Dtos;

public class BaseResponse<T> : BaseResponse
{
    public BaseResponse(T result, int status = 200, string message = "")
    {
        Results = result;
        Status = status;
        Message = message;
    } 
    public T Results { get; set; }
}

 
    public class BaseResponse(int status = 200, string message = "")
    {
      
        public int  Status{
            get;
            protected  set;
        }=status;
        
        public string  Message{get; set; }=message;
        public IResult ToResult()=>TypedResults.Json(this,statusCode:Status); 
    } 
     
 