import './readbtn.css'

export default function ReadButton(props){

    return(

        <button onClick={props.onClick} className='btn-read'>{props.isActive ? "скрыть" : "читать"}</button>
    )
}