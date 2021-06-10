import React, {useState, useEffect} from 'react';


let Component = (props) => 
{
    const [state, setState] = useState([]);
    const [review, setReview] = useState({"rating":props.review.rating});
    const [editedReviews, setEditedReviews] = useState([]);

    useEffect(() => {
        setState(props.review);
    }, [props.review]);

    const handleChange = (event) => {
        let newState = { ...state };

        newState[event.target.name] = event.target.value;

        setState(newState);
    };


    const editReviews = (reviews) => {
        setEditedReviews(reviews);
    }

    return (
        <div className="form">
            <h3 className="form-header">Editing Review for Destination{" " + props.review.destinationID}</h3>

            <div className="form-field">
                                    <label htmlfor="destination">Destination</label>
                                    <label htmlfor="destination">{props.review.destinationID}</label>
                                </div>
            <div className="form-field">
                <label htmlFor="rating">Rating</label>
                <div class="wrapper">
                    <input name="rating" type="radio" id="st1" value="1" />
                    <label for="st1"></label>
                    <input name="rating" type="radio" id="st2" value="2" />
                    <label for="st2"></label>
                    <input name="rating" type="radio" id="st3" value="3" />
                    <label for="st3"></label>
                    <input name="rating" type="radio" id="st4" value="4" />
                    <label for="st4"></label>
                    <input name="rating" type="radio" id="st5" value="5" />
                    <label for="st5"></label>
                </div>
            </div>
            <div className="form-field">
                <label htmlFor="description">Description</label>
                <input type="text" className="form-control inputs" onChange={handleChange} defaultValue={props.review.description}/>
            </div>
            <form onSubmit={(event) => {event.preventDefault(); props.handleEdit(state, editReviews);}}>
                <button className="btn btn-primary btn-submit" type="submit">Confirm Edit</button><br/>
                <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
            </form>

        </div>
    );
}
export default Component;