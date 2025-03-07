using FillingPointsHandler.Models;

namespace FillingPointsHandler.Handler;

public class SupermarketHandler(string _id) : IFillingPointHandler
{

    /// <summary>
    /// 
    /// </summary>
    /// <return>
    /*
     <TimeSeries xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
       <Id>Test</Id>
       <Scans>
         <Gebinde>
           <Materaial>00001</Materaial>
           <Batch>4711</Batch>
           <Time>2025-01-01T11:00:00</Time>
           <Amount>120</Amount>
         </Gebinde>
         <Gebinde>
           <Materaial>00001</Materaial>
           <Batch>4711</Batch>
           <Time>2025-01-01T12:00:00</Time>
           <Amount>120</Amount>
         </Gebinde>
       </Scans>
     </TimeSeries>
     */
    /// </return>
    public List<Gebinde> Calc(DateTime start, DateTime end)
    {
        //FIFO
        return StackHelper
            .TryGetByTimeSpan(_id, start, end)
            .GebindeStack;
    }
}