import { useState, useEffect } from 'react';

// Ki?u d? li?u cho document
interface DocumentData {
  id: string | number;
  name: string;
}

// Custom hook l?y d? li?u tài li?u (document)
function useDocument(documentId: string | number) {
  const [document, setDocument] = useState<DocumentData | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    if (!documentId) return;
    setLoading(true);
    // Gi? l?p g?i API l?y d? li?u tài li?u
    setTimeout(() => {
      setDocument({ id: documentId, name: `Document #${documentId}` });
      setLoading(false);
    }, 1000);
  }, [documentId]);

  return { document, loading };
}

export default useDocument;
