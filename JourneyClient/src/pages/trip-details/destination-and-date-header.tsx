import { MapPin, Calendar, Settings2 } from "lucide-react";
import { Button } from "../../components/button";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { api } from "../../lib/axios";

interface Trip {
  id: string;
  destination: string;
  starts_at: string;
  ends_at: string;
  is_confirmed: boolean;
}

export function DestinationAndDateHeader () {
  const { id } = useParams();
  const [trip, setTrip] = useState<Trip | undefined>();

  useEffect(() => {
    api.get(`trips/${id}`).then(response => setTrip(response.data.trip));
  }, [id])
  
  return (
    <div className="px-4 h-16 rounded-xl bg-zinc-900 shadow-shape flex items-center justify-between">
      <div className="flex items-center gap-2">
        <MapPin className="size-5 text-zinc-400" />
        <span className="text-zinc-100">{trip?.destination}</span>
      </div>

      <div className="flex items-center gap-5">
        <div className="flex items-center gap-2">
          <Calendar className="size-5 text-zinc-400" />
          <span className="text-zinc-100">17 a 23 de Agosto</span>
        </div>

        <div className="w-px h-6 bg-zinc-800" />

        <Button theme="secondary">
          Alterar local/data
          <Settings2 className="size-5" />
        </Button>
      </div>
    </div>
  )
}