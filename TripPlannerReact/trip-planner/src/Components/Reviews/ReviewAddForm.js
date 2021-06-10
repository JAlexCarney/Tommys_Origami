import React, {useState, useEffect} from 'react';


<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"></link>

let Component = (props) => 
{
    const [state, setState] = useState([]);
    const [review, setReview] = useState({"destinationID":-1,"rating":0,"description":"Great Trip!"});
    const [reviews, setReviews] = useState([]);
    const [updateReviews, setUpdateReviews] = useState([]);

    useEffect(() => {
        const headers = {
          method: "GET",
          headers: {
              "Accept": "application/json",
              "Authorization": `Bearer ${props.token}` 
          }
      };
      
        fetch("https://localhost:44365/api/reports/mostvisited", headers)
            .then(response => {
                if (response.status !== 200) {
                    console.log(`Bad status: ${response.status}`);
                    return Promise.reject("response is not 200 OK");
                }
                return response.json();
            })
            .then(json => setState(json))
            .catch(console.log);
      }, []);

    //getting all of the destinationTrips for the edit.  
    /*
    useEffect(() => {
        const headers = {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": `Bearer ${props.token}` 
            }
        };
        let url = "https://localhost:44365/api/reviews/getbyuser/" + props.userID;
        fetch(url, headers)
            .then(response => {
                if (response.status !== 200) {
                    console.log(`Bad status: ${response.status}`);
                    return Promise.reject("response is not 200 OK");
                }
                return response.json();
            })
            .then(json => setUpdateReviews(json))
            .catch(console.log);
    }, []);
    */

    const handleChange = (event) => {
        let newReview = { ...review };

        newReview[event.target.name] = event.target.value;

        setReview(newReview);
    };

    const handleCheck = (event) => {
        let newState = { ...state };
        if(event.target.value === "on")
        {
            newState[event.target.name] = true;
        }
        else
        {
            newState[event.target.name] = false;
        }
        setState(newState);
    }

    let handleSelection = (event) => {
        
        let newReview = { ...review };
        newReview["destinationID"] = event.target.value;
        setReview(newReview);
    }

    let destinationOptions = (list) =>
    {
        return list.map((destination) => {
            return(
                <option name="id" value={destination.destinationID} >{destination.city + " - " + destination.country}</option>
                
            );
        });
    }

    return (
        <div className="form">
            <h3 className="form-header">Adding Review</h3>
        <form onSubmit={(event) => {event.preventDefault(); console.log(review); props.handleAdd(review);}}>
            <div className="form-field">
                                    <label htmlFor="destination">Destination</label>
                                    <select onChange={handleSelection}>
                                        <option selected disabled>Choose Destination</option>
                                        {destinationOptions(state)}
                                    </select>
                                </div>
            <div className="form-field">
                <label htmlFor="rating">Rating</label>
                <div className="wrapper">
                    
                    <input name="rating" onChange={handleChange} type="radio" id="st1" value="5" />
                    <label className="radio-inline" htmlFor="st1"></label>
                    <input name="rating" onChange={handleChange} type="radio" id="st2" value="4" />
                    <label className="radio-inline" htmlFor="st2"></label>
                    <input name="rating" onChange={handleChange} type="radio" id="st3" value="3" />
                    <label className="radio-inline" htmlFor="st3"></label>
                    <input name="rating" onChange={handleChange} type="radio" id="st4" value="2" />
                    <label className="radio-inline" htmlFor="st4"></label>
                    <input name="rating" onChange={handleChange} type="radio" id="st5" value="1" />
                    <label className="radio-inline" htmlFor="st5"></label>
                </div>
            </div>
            <div className="form-field">
                <label htmlFor="description">Description</label>
                <input type="text" name="description" className="form-control inputs" onChange={handleChange} defaultValue={"Great trip!"}/>
            </div>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Add</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;