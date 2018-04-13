using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BxFin.Controllers
{
    public class ArrayCalcController : ApiController
    {
        //Get api/arraycalc
        [HttpGet]
        [Route("api/arraycalc/reverse")]
        public IHttpActionResult reverse([FromUri]int[] productIds)
        {
            try
            {
                if (null == productIds)
                    return BadRequest("Array cannot be empty");
                else
                {
                    var arrayLength = productIds.Length;
                    //Replace first half of the array with the last half.
                    for (int i = 0; i < arrayLength / 2; i++)
                    {
                        //Swap Values between array[i] and array[n-1], where n is the last element of the array.
                        productIds[i] = productIds[arrayLength - 1 - i] + productIds[i];
                        productIds[arrayLength - 1 - i] = productIds[i] - productIds[arrayLength - 1 - i];
                        productIds[i] = productIds[i] - productIds[arrayLength - 1 - i];
                    }
                    return Ok(productIds);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/arraycalc/deletepart")]
        public IHttpActionResult deletepart([FromUri]int position, [FromUri] int[] productIds)
        {
            try
            {
                if (null == productIds)
                    return BadRequest("Array cannot be empty");
                if (position > productIds.Length)
                    return BadRequest($"No element at position {position}");
                //Have to create a new array of length less than productIds
                var returnArray = new int[productIds.Length - 1];
                position = position - 1;
                int j = 0;
                for (int i = 0; i < productIds.Length; i++)
                {
                    if (position != i)
                    {
                        returnArray[j] = productIds[i];
                        j++;
                    }
                }
                return Ok(returnArray);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
