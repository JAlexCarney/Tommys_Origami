import React, {useState, useEffect} from 'react';


let Component = (props) => 
{
    const [state, setState] = useState([]);
    const [review, setReview] = useState({"rating":props.review.rating, "description":props.review.description, "destinationID":props.review.destinationID});
    const [editedReviews, setEditedReviews] = useState([]);

    useEffect(() => {
        setState(props.review);
    }, [props.review]);

    const handleChange = (event) => {
        console.log(event.target.name);
        console.log(event.target.value);
        let newReview = { ...review };

        newReview[event.target.name] = event.target.value;

        setReview(newReview);
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
                    <input className="label-small" name="rating" type="radio" id="st1" value="5" onChange={handleChange}/>
                    <label className="label-small" for="st1"></label>
                    <input className="label-small" name="rating" type="radio" id="st2" value="4" onChange={handleChange}/>
                    <label className="label-small" for="st2"></label>
                    <input className="label-small" name="rating" type="radio" id="st3" value="3" onChange={handleChange}/>
                    <label className="label-small" for="st3"></label>
                    <input className="label-small" name="rating" type="radio" id="st4" value="2" onChange={handleChange}/>
                    <label className="label-small" for="st4"></label>
                    <input className="label-small" name="rating" type="radio" id="st5" value="1" onChange={handleChange}/>
                    <label className="label-small" for="st5"></label>
                </div>
            </div>
            <div className="form-field">
                <label htmlFor="description">Description</label>
                <input type="text" name="description" className="form-control inputs" onChange={handleChange} defaultValue={props.review.description}/>
            </div>
            <form onSubmit={(event) => {event.preventDefault(); props.handleEdit(review, editedReviews);}}>
                <button className="btn btn-primary btn-submit" type="submit">Confirm Edit</button><br/>
                <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
            </form>

        </div>
    );
}
export default Component;