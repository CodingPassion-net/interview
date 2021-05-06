export class Customer {

    rentals: Rental[] = [];

    constructor(private name: String) { }

    addRental(rental: Rental) {
        this.rentals.push(rental);
    }

    generateReport() {
        let totalAmount = 0;
        let frequentRenterPoints = 0;

        this.rentals.forEach(rental => {
            const amount = rental.getRentalAmount();
            frequentRenterPoints += rental.getRenterPoints();

            totalAmount += amount;
        });

        ReportGenerator.generateReport(this.name, this.rentals, totalAmount, frequentRenterPoints);
    }
}

class ReportGenerator {
    static generateReport(name: String, rentals: Rental[], totalAmount, frequentRenterPoints) {

        let result = 'Rental record for ' + name + '\n';

        rentals.forEach(rental => {
            const amount = rental.getRentalAmount();

            // show figures for this rental
            result += '\t' + rental.getMovieTitle() + '\t' + amount.toString() + '\n';

            totalAmount += amount;
        });

        result += 'Amount owed is ' + totalAmount.toString() + '\n';
        result += 'You earned ' + frequentRenterPoints.toString() + ' frequent renter points';

        return result;
    }
}

interface Movie {
    getTitle(): string;
    getPoints(days: number): number;
    calculateRentalAmount(days: number): number;
}

export abstract class MovieClass {
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

class RegularMovie extends MovieClass implements Movie {
    getPoints(days: number): number {
        return 1;
    }
    calculateRentalAmount(days: number) {
        let amount = 2;
        if (days > 2) {
            amount += (days - 2) * 1.5;
        }
        return amount;
    }
}
class NewReleaseMovie extends MovieClass implements Movie {
    getPoints(days: number): number {
        return days > 1 ? 2 : 1;
    }
    calculateRentalAmount(days: number) {
        return days * 3;
    }
}

class ChildrenMovie extends MovieClass implements Movie {
    getPoints(days: number): number {
        return 1;
    }
    calculateRentalAmount(days: number) {
        let amount = 1.5;
        if (days > 3) {
            amount += (days - 3) * 1.5;
        }
        return amount;
    }
}


export class Rental {
    daysRented: number;

    constructor(private movie: Movie) {
    }

    getRentalAmount(): number {
        return this.movie.calculateRentalAmount(this.daysRented);
    }

    getRenterPoints(): number {
        return this.movie.getPoints(this.daysRented);
    }

    getMovieTitle(): string {
        return this.movie.getTitle();
    }
}