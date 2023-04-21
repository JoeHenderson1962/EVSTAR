

class DatePicker
{
    constructor(elem) {
        this.root = elem;
        this.root.setAttribute('autocomplete', 'off');
        this.calender = document.createElement('div');
        this.calender.classList.add('calender');
        document.body.appendChild(this.calender);
        let template = `
            <div class="sp-cal-title">
            <a>&Lang;</a>
            <a>&lang;</a>
            <span class="cal-caption"></span>
            <a>&rang;</a>
            <a>&Rang;</a>
            </div>
            <div class="sp-cal-label-days">
                <span>SUN</span>
                <span>MON</span>
                <span>TUE</span>
                <span>WED</span>
                <span>THU</span>
                <span>FRI</span>
                <span>SAT</span>
            </div>
            <div class="sp-cal-days"></div>
        `;
        this.calender.innerHTML = template;
        this.daysDiv = this.calender.getElementsByClassName('sp-cal-days')[0];
        this.daysList = [];
       this.isCalenderVisible = false;
        this.mouseX = 0;
        this.mouseY = 0;
        let today = new Date();
        this.month = today.getMonth();
        this.year = today.getFullYear();
        this.markedDay = 0;
        this.markedMonth = 0;
        this.markedYear = 0;
        this.root.addEventListener('focus' , e => {
            this.showCalender();
        });
        document.addEventListener('click', e => {
            if(this.isCalenderVisible) {
                this.mouseX = e.clientX;
                this.mouseY = e.clientY;
                let rect = this.calender.getBoundingClientRect();
                if(this.mouseX > rect.left && this.mouseX < rect.right)
                    if(this.mouseY > rect.top && this.mouseY < rect.bottom)
                        return;
                this.hideCalender();
            }
        });
        this.daysDiv.addEventListener('click', e => {
            let bRect = this.daysDiv.getBoundingClientRect();
            let nRows = this.daysList.length / 7;
            let dHeight = bRect.height/ nRows; 
            let dWidth = bRect.width / 7;
            let yIndex = Math.floor( (e.clientY - bRect.top)/dHeight);
            let xIndex = Math.floor( (e.clientX - bRect.left)/dWidth);
            let day = this.daysList[yIndex * 7 + xIndex];
            if(day) {
                this.markedDay = day;
                this.markedMonth = this.month;
                this.markedYear = this.year;
                let d = String(day);
                let m = String(this.month + 1);
                let y = String(this.year);
                if ( d.length  == 1) d = '0' + d;
                if  (m.length == 1) m = '0' + m;
                this.root.value = `${m}/${d}/${y}`;
                this.hideCalender();
            }
        });
        let capBtns = this.calender.getElementsByClassName('sp-cal-title')[0];
        capBtns = capBtns.getElementsByTagName('a');
        capBtns[0].addEventListener('click', e=> {
            this.year--;
            this.renderCalender();
        })
        capBtns[1].addEventListener('click', e=> {
            this.month--;
            if(this.month < 0){
                this.month = 11;
                this.year --;
            }
            this.renderCalender();
        })
        capBtns[2].addEventListener('click', e=> {
            this.month++;
            if(this.month > 11)
            {
                this.month = 0;
                this.year++;
            } 
            this.renderCalender();
        })
        capBtns[3].addEventListener('click', e=> {
            this.year++;
            this.renderCalender();
        })
    }

    showCalender() {
        setTimeout(() => {
            this.isCalenderVisible = true;
        }, 200);
        this.calender.style.visibility =  'visible';
        let {height, top, left} = this.root.getBoundingClientRect();
        let cRect = this.calender.getBoundingClientRect();
        let y = top +  pageYOffset;
        let x = left + pageXOffset;
        let offset = 4 + height;
        
        if( top + cRect.height + height > window.innerHeight ) {
            offset = - 4 - cRect.height;
        }
        this.calender.style.top = (y + offset) + 'px';
        this.calender.style.left = x + 'px';
        this.renderCalender();
    }
    hideCalender() {
        this.isCalenderVisible = false;
        this.calender.style.visibility = 'hidden';
    }

    renderCalender()
    {
        let cap = this.calender.getElementsByClassName('cal-caption')[0];
        let months =[
            'Jan',
            'Feb',
            'Mar',
            'Apr',
            'May',
            'Jun',
            'Jul',
            'Aug',
            'Sep',
            'Oct',
            'Nov',
            'Dec'
        ];
        cap.innerText = `${months[this.month]}, ${this.year}`
        let today = new Date();
        let nDays = new Date(this.year, this.month + 1, 0).getDate();
        let startDay = new Date(this.year, this.month, 1).getDay();
        this.daysList = [];
        for(let i = 0; i < startDay - 1; i++)
        {
            this.daysList.push(0);
        }
        for(let i = 0; i <= nDays; i++)
        {
            this.daysList.push(i);
        }
        this.daysDiv.innerHTML = '';
        for(let d of this.daysList) {
            let b = document.createElement('a');
            if(this.month == today.getMonth() && d == today.getDate() &&this.year == today.getFullYear())
            {
                b.style.backgroundColor = '#ddd';
                console.log(d);
            }
            if( this.markedDay > 0 )
            {
                if(this.markedYear == this.year && this.markedMonth == this.month && d == this.markedDay)
                {

                    b.style.backgroundColor = '#2979ff';
                    b.style.color = '#fff';
                }
            }
            b.innerText = d === 0 ? '' : `${d}`;
            this.daysDiv.appendChild(b);
        }

    }
}