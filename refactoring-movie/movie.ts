export class Customer {

    name: string;
    rentals: Rental[] = [];

    constructor(private objMovie: Movie) {
    }

    getName() {
        return this.name;
    }

    addRental(rental: Rental) {
        this.rentals.push(rental);
    }

    statement() {
        let totalAmount = 0;
        let frequentRenterPoints = 0;

        let result = 'Rental record for ' + this.getName() + '\n';
        for (let i = 0; i < this.rentals.length; i++) {
            let amount = 0;
            const element = this.rentals[i];
            switch (element.getMovie().getPriceCode()) {
                case this.objMovie.REGULAR:
                    amount += 2;
                    if (element.getDaysRented() > 2) {
                        amount += (element.getDaysRented() - 2) * 1.5;
                    }
                    break;
                case this.objMovie.NEW_RELEASE:
                    amount += element.getDaysRented() * 3;
                    break;
                case this.objMovie.CHILDREN:
                    amount += 1.5;
                    if (element.getDaysRented() > 3) {
                        amount += (element.getDaysRented() - 3) * 1.5;
                    }
                    break;
            }

            // add frequent renter points
            frequentRenterPoints++;

            // add bonus for a two day new release rental
            if (element.getMovie().getPriceCode() == this.objMovie.NEW_RELEASE && element.getDaysRented() > 1) {
                frequentRenterPoints++;
            }

            // show figures for this rental
            result += '\t' + element.getMovie().getTitle() + '\t' + amount.toString() + '\n';

            totalAmount += amount;
        }

        result += 'Amount owed is ' + totalAmount.toString() + '\n';
        result += 'You earned ' + frequentRenterPoints.toString() + ' frequent renter points';

        return result;
    }

}

export class Movie {
    CHILDREN: number = 2;
    REGULAR: number = 0;
    NEW_RELEASE: number = 1;

    title: string;
    priceCode: number;

    constructor() {
    }

    getTitle() {
        return this.title;
    }

    getPriceCode() {
        return this.priceCode;
    }

    setPriceCode(priceCode: number) {
        this.priceCode = priceCode;
    }
}

export class Rental {
    movie: Movie;
    daysRented: number;

    constructor() {
    }

    getMovie() {
        return this.movie;
    }

    getDaysRented() {
        return this.daysRented;
    }
}