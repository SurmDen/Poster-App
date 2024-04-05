import './button.css'

export default function Button(props){

    return(
        <button onClick={props.onClick} style={{marginRight:props.mr}} className={props.isActive ? 'btn active-btn' : 'btn'}>
            {props.children}
        </button>
    )
}