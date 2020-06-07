import java.util.*

class Customer(private val name: String) {
    private val rentals: MutableList<Rental> = ArrayList()

    fun addRental(rental: Rental) {
        rentals.add(rental)
    }

    fun statement(): String {
        var totalAmount = 0.0
        var frequentRenterPoints = 0
        var result = "Rental record for $name\n"
        for (rental in rentals) {
            var amount = 0.0
            when (rental.movie.priceCode) {
                Movie.REGULAR -> {
                    amount += 2.0
                    if (rental.daysRented > 2)
                        amount += (rental.daysRented - 2) * 1.5
                }
                Movie.NEW_RELEASE -> amount += rental.daysRented * 3.toDouble()
                Movie.CHILDREN -> {
                    amount += 1.5
                    if (rental.daysRented > 3)
                        amount += (rental.daysRented - 3) * 1.5
                }
            }

            // add frequent renter points
            frequentRenterPoints++

            // add bonus for a two day new release rental
            if (rental.movie.priceCode == Movie.NEW_RELEASE && rental.daysRented > 1)
                frequentRenterPoints++

            // show figures for this rental
            result += """ ${rental.movie.title} $amount"""
            totalAmount += amount
        }
        result += "Amount owed is $totalAmount\n"
        result += "You earned $frequentRenterPoints frequent renter points"
        return result
    }
}

class Movie(val title: String, var priceCode: Int) {
    companion object {
        const val CHILDREN = 2
        const val REGULAR = 0
        const val NEW_RELEASE = 1
    }
}

class Rental(val movie: Movie, val daysRented: Int)
